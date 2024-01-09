using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Zoo_Animal_Management_System.Controllers;
using Zoo_Animal_Management_System.Requests;
using Zoo_Animal_Management_System.Services;

namespace ZooAnimalManagementSystemTEst
{
    public class ZooAnimalFileUploadControllerTests
    {
        private readonly ZooAnimalFileUploadController _controller;
        private readonly Mock<IZooAnimalFileUploaderService> _fileUploaderServiceMock;

        public ZooAnimalFileUploadControllerTests()
        {
            _fileUploaderServiceMock = new Mock<IZooAnimalFileUploaderService>();
            _controller = new ZooAnimalFileUploadController(_fileUploaderServiceMock.Object);
        }





    }
}
