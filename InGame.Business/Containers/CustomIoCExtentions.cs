﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InGame.Business.Concrete.DTO.Concrete.User;
using InGame.Business.Concrete.Manager;
using InGame.Business.Interface;
using InGame.Business.Tools.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace InGame.Business.Containers
{
    public static class CustomIoCExtentions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            #region Validation

            services.AddTransient<IValidator<UserRegisterDto>, UserRegisterValidator>();

            #endregion

            #region DependencyInjection

            services.AddScoped<IUserService, UserManager>();

            #endregion
        }
    }
}