using System;
using System.Collections.Generic;
using Admin.Extentions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musical_WebStore_BlazorApp.Shared;
using Musical_WebStore_BlazorApp.Client;
using Musical_WebStore_BlazorApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;
using Admin.ResultModels;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ChatsController(MusicalShopIdentityDbContext ctx, UserManager<User> userManager, IMapper mapper)
        {
            this.ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
        }
        private Task<Chat[]> GetChatsAsync() => ctx.Chats.ToArrayAsync();

        [Route("getchats/{email}")]
        public async Task<ChatModel[]> GetChats(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var chats = await GetChatsAsync();
            var chatsmodels = chats.Where(c => c.ChatUsers.Select(cu => cu.UserId).Contains(user.Id)).Select(c => _mapper.Map<ChatModel>(c)).ToArray();
            return chatsmodels;
        }
        [Route("getchatsforservice")]
        public async Task<ChatModel[]> GetChatsForService()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var serviceid = ctx.ServiceUsers.Single(us => us.UserId == user.Id).ServiceId;
            var chats = await GetChatsAsync();
            var chatsmodels = chats.Where(c => c.ServiceId == serviceid)
            .Where(cm => ctx.ChatUsers.Where(cu => cu.ChatId == cm.Id).Count() == 1)
            .Select(c => _mapper.Map<ChatModel>(c))
            .ToArray();
            return chatsmodels;
        }
        public Task<Message[]> GetMessagesAsync() => ctx.Messages.ToArrayAsync();
        [Route("getmessages")]
        public async Task<MessageModel[]> GetMessages()
        {
            var messages = await GetMessagesAsync();
            var messagesmodels = messages.Select(m => _mapper.Map<MessageModel>(m)).ToArray();
            return messagesmodels;
        }
        [HttpPost]
        [Route("addmessage")]
        public async Task<AddMessageResult> AddMessage(AddMessageModel model)
        {
            var mes = _mapper.Map<Message>(model);
            mes.Date = DateTime.Now;
            var email = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(email);
            mes.UserId = user.Id;
            ctx.Messages.Add(mes);
            await ctx.SaveChangesAsync();
            return new AddMessageResult() {Success = true};
        }

        [Route("addchat")]
        public async Task<Result> AddChat(AddChatModel model)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var chat = new Chat()
            {
                Name = user.GetCompany(ctx).Name + " - " + ctx.Services.Single(s => s.Id == model.ServiceId).Name,
                Description = "—",
                ServiceId = model.ServiceId,
                CompanyId = user.GetCompany(ctx).Id
            };
            ctx.Chats.Add(chat);
            ctx.SaveChanges();
            ctx.ChatUsers.Add(
                new ChatUser()
                {
                    UserId = user.Id,
                    ChatId = chat.Id
                }
            );
            ctx.SaveChanges();
            ctx.Messages.Add(
                new Message()
                {
                    Date = DateTime.Now,
                    ChatId = chat.Id,
                    Text = "Conversation was started",
                }
            );
            ctx.SaveChanges();
            return new Result() { Successful = true};

        }

        [Route("joinchat")]
        public async Task<Result> JoinChat(JoinChatModel model)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            ctx.ChatUsers.Add
            (
                new ChatUser()
                {
                    ChatId = model.ChatId,
                    UserId = user.Id
                }
            );
            ctx.Messages.Add(
                new Message()
                {
                    ChatId = model.ChatId,
                    Text = $"{user.UserName} joined chat."
                }
            );
            await ctx.SaveChangesAsync();
            
            return new Result() {Successful = true};
        }
        [Route("getchatusers/{chatId}")]
        public async Task<ChatUserModel[]> GetChatUsers(int chatId)
        {
            var cuModels = ctx.ChatUsers.Where(cu => cu.ChatId == chatId).Select(cu => _mapper.Map<ChatUserModel>(cu)).ToArray();
            return cuModels;
        }
        [Route("getusers")]
        public async Task<UserLimited[]> GetUsers()
        {
            var users = await _userManager.Users.ToArrayAsync();
            var res = users.Select(u => _mapper.Map<UserLimited>(u)).ToArray();
            return res;
        }

    }
}