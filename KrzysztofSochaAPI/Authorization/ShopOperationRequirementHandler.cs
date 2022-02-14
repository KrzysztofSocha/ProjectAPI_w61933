using KrzysztofSochaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Authorization
{
    public class ShopOperationRequirementHandler:AuthorizationHandler<ResourceOperationRequirement, Shop>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
           ResourceOperationRequirement requirement,
           Shop shop)
        {
           
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;           

            switch (requirement.ResourceOperation)
            {
                case ResourceOperation.Create:
                    if(userRole=="Admin"||userRole=="Manager")
                        context.Succeed(requirement);
                    break;
                case ResourceOperation.Read:
                    context.Succeed(requirement);
                    break;
                case ResourceOperation.Update:
                    if(int.Parse(userId) == shop.ManagerId || userRole == "Admin")
                        context.Succeed(requirement);
                    break;
                case ResourceOperation.Delete:
                    if (userRole == "Admin")                    
                        context.Succeed(requirement);                    
                    break;
                default:
                    context.Fail();
                    break;
            }


            return Task.CompletedTask;
        }
    }
}
