﻿using System.Threading.Tasks;
using FloydPink.Flickr.Downloadr.Model.Enums;

namespace FloydPink.Flickr.Downloadr.Presentation
{
  public interface ILandingPresenter
  {
    Task Initialize();
    Task NavigateTo(PhotoOrAlbumPage page);
    Task NavigateTo(int page);
  }
}
