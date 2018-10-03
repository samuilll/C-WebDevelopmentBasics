using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GamesStoreData.Models;
using GamesStoreData.Models.ViewModels;

namespace SIS.GameStoreApp.Common
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<RegisterViewModel, User>();
        }
    }
}
