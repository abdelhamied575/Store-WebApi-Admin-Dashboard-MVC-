using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Helper
{
	public  class DocumentSettings
	{

		public static string UploadFile(IFormFile file, string folderName)
		{
			try
			{
				// 1. Define folder path and ensure it exists
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folderName);
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				// 2. Generate a unique file name
				var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

				// 3. Get the full file path
				var filePath = Path.Combine(folderPath, fileName);

				// 4. Copy file to destination
				using var fileStream = new FileStream(filePath, FileMode.Create);
				file.CopyTo(fileStream);

				return $"images/{folderName}/{fileName}"; // Return relative path for later access
			}
			catch (Exception ex)
			{
				// Log or handle the exception as needed
				Console.WriteLine($"Error uploading file: {ex.Message}");
				return null; // Consider returning an error message or throwing an exception
			}
		}

		public static bool DeleteFile(string imageUrl, string folderName)
		{
			try
			{
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var filePath = Path.Combine(folderPath, imageUrl);

				// Check if file exists and delete it
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					return true;
				}

				Console.WriteLine("File not found for deletion.");
				return false; // File was not found
			}
			catch (Exception ex)
			{
				// Log or handle the exception as needed
				Console.WriteLine($"Error deleting file: {ex.Message}");
				return false;
			}
		}

	}
}
