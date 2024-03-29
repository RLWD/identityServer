﻿using IdentityModel;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VDW.SalesApp.IdentityServer.Models;

namespace VDW.SalesApp.IdentityServer.Services
{
	public class ResourceOwnerServices : IResourceOwnerPasswordValidator
	{
		public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			var claimList = new List<Claim>()
			{
				new Claim(UserClaimKeys.UserId, context.Request.Raw["UserId"]),
				new Claim(UserClaimKeys.EnglishName, context.Request.Raw["EnglishName"]),
				new Claim(UserClaimKeys.ChineseName, context.Request.Raw["ChineseName"]),
				new Claim(UserClaimKeys.PhoneNumber, context.Request.Raw["PhoneNumber"]),
				new Claim(UserClaimKeys.Email, context.Request.Raw["Email"]),
				new Claim(UserClaimKeys.IsActive, context.Request.Raw["IsActive"]),
				new Claim(UserClaimKeys.UserCode, context.Request.Raw["UserCode"]),
            };
			if (!string.IsNullOrEmpty(context.Request.Raw["CustomerUserRelationshipHash"]))
				claimList.Add(new Claim(UserClaimKeys.CustomerUserRelationshipHash, context.Request.Raw["CustomerUserRelationshipHash"]));
            if (!string.IsNullOrEmpty(context.Request.Raw["WechatUserId"]))
                claimList.Add(new Claim(UserClaimKeys.WechatUserId, context.Request.Raw["WechatUserId"]));
            if (!string.IsNullOrEmpty(context.Request.Raw["Wechat"]))
                claimList.Add(new Claim(UserClaimKeys.WechatPermission, context.Request.Raw["Wechat"]));
			if (!string.IsNullOrEmpty(context.Request.Raw["CustomerUserId"]))
				claimList.Add(new Claim(UserClaimKeys.CustomerUserId, context.Request.Raw["CustomerUserId"]));

			context.Result = new GrantValidationResult(
				subject: context.Request.Raw["PhoneNumber"],
				authenticationMethod: OidcConstants.GrantTypes.Password,
				claims: claimList);
			
		}
	}
}