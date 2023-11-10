using AnagramWS.Controllers;
using AnagramWS.DTOs;
using AnagramWS.Exceptions;
using AnagramWS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramWS.Test.Controllers;

public class AnagramControllerTest
{
    [Fact]
    public void Find_ReturnsOkObjectResult()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.FindAnagrams("chien")).Returns(new string[] {"niche", "chine"});
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Find("chien");
        
        var actionResult = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
        Assert.Equal(2, model.Count());
    }
    
    [Fact]
    public void Find_ThrowsException_ReturnsInternalServerError()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.FindAnagrams("chien")).Throws(new Exception());
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Find("chien");
        
        var actionResult = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
        var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public void Add_WithValidWord_ReturnsCreatedObjectResult()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Add("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Add(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var createdResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }
    
    [Fact]
    public void Add_WithEmptyWord_ReturnsBadRequest()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Add("chien")).Throws(new WordAlreadyExistsException("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Add(new WordDTO{ Word = "" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public void Add_ThrowsWordAlreadyExistsException_ReturnsBadRequest()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Add("chien")).Throws(new WordAlreadyExistsException("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Add(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public void Add_ThrowsException_ReturnsInternalServerError()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Add("chien")).Throws(new Exception());
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Add(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public void Remove_WithValidWord_ReturnsOkObjectResult()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Remove("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Remove(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public void Remove_WithEmptyWord_ReturnsBadRequest()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Remove("chien")).Throws(new WordNotFoundException("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Remove(new WordDTO{ Word = "" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public void Remove_ThrowsWordNotFoundException_ReturnsBadRequest()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Remove("chien")).Throws(new WordNotFoundException("chien"));
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Remove(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public void Remove_ThrowsException_ReturnsInternalServerError()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.Remove("chien")).Throws(new Exception());
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Remove(new WordDTO{ Word = "chien" });
        
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public void Count_ReturnsOkObjectResult()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.CountWordsWithAnagrams()).Returns(1);
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Count();
        
        var actionResult = Assert.IsType<ActionResult<int>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public void Count_ThrowsException_ReturnsInternalServerError()
    {
        var repository = new Mock<IWordRepository>();
        repository.Setup(r => r.CountWordsWithAnagrams()).Throws(new Exception());
        var controller = new AnagramController(repository.Object);
        
        var result = controller.Count();
        
        var actionResult = Assert.IsType<ActionResult<int>>(result);
        var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}