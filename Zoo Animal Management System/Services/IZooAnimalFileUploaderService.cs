using Microsoft.AspNetCore.Mvc;
using Zoo_Animal_Management_System.Dto;
using Zoo_Animal_Management_System.Requests;

namespace Zoo_Animal_Management_System.Services
{
    public interface IZooAnimalFileUploaderService
    {
        Task<IActionResult> UploadFile<TDto>(string fileNamePrefix, FileUploadRequest request);
    }
}