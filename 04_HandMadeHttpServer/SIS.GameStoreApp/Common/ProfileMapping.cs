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

            CreateMap<GameToAddViewModel, Game>();

            CreateMap<Game,GameViewModel>();

            CreateMap<Game, GameHomeViewModel>()
                .ForMember(dest => dest.Description,
                           m => m.MapFrom(src => TakeShortDescription(src.Description)));         
        }

        private string TakeShortDescription(string description)
        {
            if (description.Length>300)
            {
                description = description.Substring(0, 300);
            }

            return description;
        }
    }
}
