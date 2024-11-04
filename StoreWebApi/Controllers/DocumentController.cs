using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Services.Helper;

namespace StoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentController : ControllerBase
	{

		[HttpPost("upload")]
		public ActionResult<string> UploadFile(IFormFile file, string folderName)
			=> DocumentSettings.UploadFile(file, folderName);


		[HttpPost("delete")]
		public ActionResult<bool> DeleteFile(string PictureUrl, string folderName)
			=> DocumentSettings.DeleteFile(PictureUrl, folderName);




	}
}
