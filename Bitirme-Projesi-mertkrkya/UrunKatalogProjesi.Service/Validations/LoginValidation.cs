﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Data.Dto;
using UrunKatalogProjesi.Data.Repositories;

namespace UrunKatalogProjesi.Service.Validations
{
    public class LoginValidation : AbstractValidator<LoginRequest>
    {
        public LoginValidation()
        {

            RuleFor(r => r.UserName)
                .NotNull().WithMessage("UserName cannot be null.")
                .NotEmpty().WithMessage("UserName cannot be empty.");
            

            RuleFor(r => r.Password)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .Length(8, 20).WithMessage("Password length must be between 8 to 20 char long.");
        }
    }
}
