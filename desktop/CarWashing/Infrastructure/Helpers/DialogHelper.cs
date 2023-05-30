using Avalonia . Controls;
using System . Collections . Generic;

namespace CarWashing . Infrastructure . Helpers {
	internal static class DialogHelper {
		public static SaveFileDialog SaveFile ( string? init = null ) => new ( ) {
			DefaultExtension="pdf" ,
			Title="Сохранение документа PDF" ,
			InitialFileName=init
			};

		public static OpenFileDialog OpenImage ( ) => new ( ) {
			AllowMultiple=false ,

			Filters=new List<FileDialogFilter> ( )
						{
										new FileDialogFilter()
										{
												Name = "Image files",
												Extensions = new List<string>() { "bmp", "jpg", "gif", "png" }
										},
										new FileDialogFilter()
										{
												Name = "PNG files",
												Extensions = new List<string>() { "png" }
										},
										new FileDialogFilter()
										{
												Name = "BMP files",
												Extensions = new List<string>() { "bmp" }
										},
										new FileDialogFilter()
										{
												Name = "GIF files",
												Extensions = new List<string>() { "gif" }
										},
										new FileDialogFilter()
										{
												Name = "JPG files",
												Extensions = new List<string>() { "jpg" }
										},
										new FileDialogFilter()
										{
												Name = "All files",
												Extensions = new List<string>() { "*" }
										}
								},
						Title="Выберите фото" ,
			};
		}
	}
