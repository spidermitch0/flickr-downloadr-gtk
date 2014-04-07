﻿using System;
using FloydPink.Flickr.Downloadr.Model;
using System.Collections.Generic;
using System.Linq;
using Gtk;

namespace FloydPink.Flickr.Downloadr
{
	public partial class BrowserWindow
	{
		private const int NUMBER_OF_PHOTOS_IN_A_ROW = 5;

		HBox AddImageToRow (HBox hboxPhotoRow, int j, Photo photo, string rowId)
		{
			var imageCell = new Image ();
			imageCell.Name = string.Format ("{0}Image{1}", rowId, j.ToString ());
			imageCell.SetCachedImage (photo.LargeSquare150X150Url);
			hboxPhotoRow.Add (imageCell);
			Box.BoxChild hboxChild = ((Box.BoxChild)(hboxPhotoRow [imageCell]));
			hboxChild.Position = j;
			hboxPhotoRow.ShowAll ();
			return hboxPhotoRow;
		}

		void SetupTheImageRow (int i, IEnumerable<Photo> rowPhotos)
		{
			var rowPhotosCount = rowPhotos.Count ();

			if (rowPhotosCount == 0) {
				return;
			}

			var rowId = string.Format ("hboxPhotoRow{0}", i.ToString ());
			var hboxPhotoRow = new global::Gtk.HBox ();
			hboxPhotoRow.Name = rowId;
			hboxPhotoRow.Spacing = 6;
			Application.Invoke (delegate {
				this.vboxPhotos.Add (hboxPhotoRow);
				Box.BoxChild vboxChild = ((Box.BoxChild)(this.vboxPhotos [hboxPhotoRow]));
				vboxChild.Position = i;
				vboxChild.Padding = (uint)15;
			});

			for (int j = 0; j < rowPhotosCount; j++) {
				hboxPhotoRow = AddImageToRow (hboxPhotoRow, j, rowPhotos.ElementAt (j), rowId);
			}
		}

		void SetupTheImageGrid (IEnumerable<Photo> photos)
		{
			var numberOfRows = photos.Count () / NUMBER_OF_PHOTOS_IN_A_ROW;
			var fullRowPhotosCount = NUMBER_OF_PHOTOS_IN_A_ROW * numberOfRows;
			var lastRowPhotosCount = photos.Count () - fullRowPhotosCount;

			Application.Invoke (delegate {
				foreach (Widget child in this.vboxPhotos.Children) {
					this.vboxPhotos.Remove (child);
				}
			});

			for (int i = 0; i <= numberOfRows; i++) {
				var rowPhotos = photos.Skip (i * NUMBER_OF_PHOTOS_IN_A_ROW).Take (i == numberOfRows ? lastRowPhotosCount : NUMBER_OF_PHOTOS_IN_A_ROW);
				SetupTheImageRow (i, rowPhotos);
			}
		}
	}
}

