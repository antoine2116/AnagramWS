using AnagramWS.DTOs;
using AnagramWS.Exceptions;
using AnagramWS.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AnagramWS.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AnagramController : ControllerBase
{
    private readonly IWordRepository _wordRepository;
    
    public AnagramController(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    /// <summary>
    /// Returns a list of anagrams for the given word.
    /// </summary>
    /// <param name="word">Word to find anagrams for</param>
    [HttpGet("{word}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<string>> Find(string word)
    {
        try
        {
            return Ok(_wordRepository.FindAnagrams(word));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while finding the anagrams."});
        }
    }

    /// <summary>
    /// Adds a word to the dictionary.
    /// </summary>
    /// <param name="wordDTO">Word to add</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> Add(WordDTO wordDTO)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(wordDTO.Word))
            {
                return BadRequest(new { Message = "The word cannot be empty." });
            }
            
            _wordRepository.Add(wordDTO.Word);
            return StatusCode(StatusCodes.Status201Created, new { Message = $"The word \"{wordDTO.Word}\" was added successfully." });
        }
        catch (WordAlreadyExistsException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while adding the word." });
        }
    }
    
    /// <summary>
    /// Removes a word from the dictionary.
    /// </summary>
    /// <param name="wordDTO">Word to remove</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<string> Remove(WordDTO wordDTO)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(wordDTO.Word))
            {
                return BadRequest(new { Message = "The word cannot be empty." });
            }
            
            _wordRepository.Remove(wordDTO.Word);
            return Ok(new { Message = $"The word \"{wordDTO.Word}\" was removed successfully." });
        }
        catch (WordNotFoundException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while removing the word." });
        }
    }
    
    /// <summary>
    /// Count the number of words in the dictionary that have anagrams.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<int> Count()
    {
        try
        {
            return Ok(new { Message = _wordRepository.CountWordsWithAnagrams() });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while counting the anagrams." });
        }
    }
}