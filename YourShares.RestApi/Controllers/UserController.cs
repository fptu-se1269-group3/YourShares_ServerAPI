﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourShares.Application.Interfaces;
using YourShares.Application.SearchModels;
using YourShares.Application.ViewModels;
using YourShares.RestApi.ApiResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YourShares.Domain.Util;

namespace YourShares.RestApi.Controllers
{
    [ApiController]
    [Route("/api/users")]
    [Produces("application/json")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userProfileService">The User profile service.</param>
        /// <returns></returns>
        public UserController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        #endregion

        #region GetById
        /// <summary>
        ///     Gets User specified by its identifier.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/user/{id}")]
        public async Task<ResponseModel<UserViewDetailModel>> GetById([FromRoute] Guid id)
        {
            var result = await _userProfileService.GetById(id);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseBuilder<UserViewDetailModel>().Success()
                .Data(result)
                .Count(1)
                .build();
        }
        #endregion

        #region Search
        /// <summary>
        ///     Search User by Email, Phone, Name.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/users")]
        public async Task<ResponseModel<List<UserSearchViewModel>>> SearchUser(
            [FromQuery] UserSearchModel model)
        {
            var result = await _userProfileService.SearchUser(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseBuilder<List<UserSearchViewModel>>().Success()
                .Data(result)
                .Count(result.Count)
                .build();
        }
        #endregion

        #region Update All profile
        /// <summary>
        ///     Updates the user information with details in the request body.
        /// </summary>
        /// <param name="model">The UserEditInfoModel.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/users")]
        public async Task UpdateInfo([FromBody] UserEditInfoModel model)
        {
            await _userProfileService.UpdateInfo(model);
            Response.StatusCode = (int)HttpStatusCode.OK;
        }
        #endregion

        #region Update patch field
        /// <summary>
        /// Update user profile info (email, phone, address)
        /// </summary>
        /// <param name="field">The field name to update</param>
        /// <param name="value">The value of field to update</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{field}")]
        public async Task Update([FromRoute] string field, [FromQuery] string value)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            switch (field)
            {
                case "email":
                    await _userProfileService.UpdateEmail(Guid.Parse(userId), value);
                    Response.StatusCode = (int) HttpStatusCode.OK;
                    break;
                case "phone":
                    await _userProfileService.UpdateInfo(new UserEditInfoModel {Phone = value});
                    Response.StatusCode = (int) HttpStatusCode.OK;
                    break;
                case "address":
                    await _userProfileService.UpdateInfo(new UserEditInfoModel {Address = value});
                    Response.StatusCode = (int) HttpStatusCode.OK;
                    break;
            }
        }
        #endregion
        
        
    }
}