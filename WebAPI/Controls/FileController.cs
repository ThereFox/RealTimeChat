using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SignalRTest;

[Controller]
[Route("/file")]
public class FileController : Controller
{
    private readonly IFileStore _fileStore;

    public FileController(IFileStore fileStore)
    {
        _fileStore = fileStore;
    }
    
    [HttpPost]
    [Route("/save/{fileName}")]
    public async Task<IActionResult> SaveFile(string fileName, CancellationToken token)
    {
        var file = new SavingFile()
        {
            FileName = fileName,
            Content = this.Request.Body,
            ContentType = this.Request.ContentType,
            Size = Request.Body.Length
        };

        if ((await _fileStore.Contain(fileName, token)).IsSuccess)
        {
            return BadRequest("file with this name exist");
        }
        
        var saveFileResult = await _fileStore.SaveFile(file, token);

        if (saveFileResult.IsFailure)
        {
            return BadRequest(saveFileResult.Error);
        }

        return Json(saveFileResult.Value);
    }

    [HttpGet]
    [Route("/get/{fileName}")]
    public async Task<IActionResult> GetFile(string fileName, CancellationToken token)
    {
        if ((await _fileStore.Contain(fileName, token)).IsFailure)
        {
            return BadRequest("have not file with this key");
        }

        var getFileResult = await _fileStore.GetFile(fileName, token);

        if (getFileResult.IsFailure)
        {
            return BadRequest(getFileResult.Error);
        }

        var fileInfo = getFileResult.Value;

        return File(fileInfo.Content, fileInfo.ContentType);
    }

    [HttpDelete]
    [Route("/delete/{fileName}")]
    public async Task<IActionResult> DeleteFile(string fileName, CancellationToken token)
    {
        if ((await _fileStore.Contain(fileName, token)).IsFailure)
        {
            return BadRequest("file was not found");
        }

        var fileDeleteResult = await _fileStore.DeleteFile(fileName, token);

        if (fileDeleteResult.IsFailure)
        {
            return BadRequest(fileDeleteResult.Error);
        }

        return Ok();
    }
}