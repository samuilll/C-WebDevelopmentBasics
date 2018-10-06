using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Domain.Models;
using IRunes.Domain.ViewModels;

namespace IRunesServices.Contracts
{
   public interface IAlbumService
   {
       void Create(AlbumToCreateViewModel model);

       List<Album> GetAll();

       AlbumDetailsView GetById(string id);

       List<Track> GetAllTracks(string album);
   }


}
