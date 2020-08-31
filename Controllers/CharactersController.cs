using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using makemagic.Data;
using makemagic.Dtos;
using makemagic.Models;
using makemagic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace makemagic.Controllers
{
  /// <summary>
  /// Character operations
  /// </summary>
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class CharactersController : ControllerBase
  {
    private readonly ICharacterRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializing the CharacterController class
    /// </summary>
    /// <param name="repository">Injected repository</param>
    /// <param name="mapper">Injected mapper</param>
    public CharactersController(
      [FromServices] ICharacterRepository repository,
      [FromServices] IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    //GET api/v1.0/characters
    /// <summary>
    /// Get all Characters
    /// </summary>
    /// <param name="house">To filter Characters by house</param>
    /// <response code="200">The Characters list was successfully retrieved</response>
    /// <response code="500">An error occurred while trying to retrieve the Characters list</response>
    /// <returns>List of Characters</returns>
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<Character>>> GetAll([FromQuery] string house)
    {
      try
      {
        List<Character> characters = new List<Character>();
        if (!string.IsNullOrEmpty(house))
        {
          characters = await _repository.GetAllCharactersByHouseAsync(house);
        }
        else
        {
          characters = await _repository.GetAllCharactersAsync();
        }

        return Ok(_mapper.Map<List<CharacterReadDto>>(characters));
      }
      catch (Exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError);
      }

    }

    //GET api/v1.0/characters/{id}
    /// <summary>
    /// Get a Character
    /// </summary>
    /// <param name="id">The ID of the Character</param>
    /// <response code="200">The Character was successfully retrieved</response>
    /// <response code="404">A character was not found for the given ID</response>
    /// <response code="500">An error occurred while trying to retrieve the Character</response>
    /// <returns>Character</returns>
    [HttpGet]
    [Route("{id}", Name = "GetById")]
    public async Task<ActionResult<Character>> GetById(int id)
    {
      try
      {
        var result = await _repository.GetCharacterByIdAsync(id);
        if (result == null)
        {
          return NotFound();
        }
        return Ok(_mapper.Map<CharacterReadDto>(result));
      }
      catch (Exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    //POST api/v1.0/characters
    /// <summary>
    /// To create a Character
    /// </summary>
    /// <param name="apiVersion">The API version</param>
    /// <param name="modeCreatelDto">The Character data</param>
    /// <response code="201">The Character was successfully created</response>
    /// <response code="400">Model validation error</response>
    /// <response code="500">An error occurred while trying to retrieve the Character</response>
    /// <returns>The updated Character</returns>
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Character>> Post(
        [FromServices] ApiVersion apiVersion,
        [FromBody] CharacterCreateDto modeCreatelDto)
    {
      try
      {
        var model = _mapper.Map<Character>(modeCreatelDto);
        if (ModelState.IsValid)
        {
          var potterApi = new PotterApi();
          var accio = await potterApi.AccioHouse(modeCreatelDto.House);
          if (accio)
          {
            await _repository.CreateCharacterAsync(model);
            await _repository.SaveChangesAsync();

            var commandReadDto = _mapper.Map<CharacterReadDto>(model);

            return CreatedAtRoute(nameof(GetById), new { Id = commandReadDto.Id, version = $"{apiVersion}" }, commandReadDto);
          }
          else
          {
            ModelState.AddModelError("Accio", "Accio House didn't work");
            return BadRequest(ModelState);
          }

        }
        else
        {
          return BadRequest(ModelState);
        }
      }
      catch (Exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    //PUT api/v1.0/characters/{id}
    /// <summary>
    /// To update Character
    /// </summary>
    /// <param name="modelUpdateDto">The Character's updated data</param>
    /// <param name="id">The Character's ID</param>
    /// <response code="204">The Character was successfully updated</response>
    /// <response code="400">Model validation error</response>
    /// <response code="404">A character was not found for the given ID</response>
    /// <response code="500">An error occurred while trying to retrieve the Character</response>
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Character>> Put([FromBody] CharacterUpdateDto modelUpdateDto, int id)
    {
      try
      {
        var character = await _repository.GetCharacterByIdAsync(id);
        if (character == null)
        {
          return NotFound();
        }
        if (ModelState.IsValid)
        {
          var potterApi = new PotterApi();
          var accio = await potterApi.AccioHouse(modelUpdateDto.House);
          if (accio)
          {
            _mapper.Map(modelUpdateDto, character);
            await _repository.SaveChangesAsync();
            return NoContent();
          }
          else
          {
            ModelState.AddModelError("Accio", "Accio House didn't work");
            return BadRequest(ModelState);
          }
        }
        else
        {
          return BadRequest(ModelState);
        }
      }
      catch (Exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    //DELETE api/v1.0/characters/{id}
    /// <summary>
    /// To delete a Character
    /// </summary>
    /// <param name="id">The Character's ID</param>
    /// <response code="204">The Character was successfully deleted</response>
    /// <response code="404">A character was not found for the given ID</response>
    /// <response code="500">An error occurred while trying to retrieve the Character</response>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        var character = await _repository.GetCharacterByIdAsync(id);
        if (character == null)
        {
          return NotFound();
        }
        _repository.DeleteCharacter(character);
        await _repository.SaveChangesAsync();

        return NoContent();
      }
      catch (Exception)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

  }
}