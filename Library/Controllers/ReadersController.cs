using Library.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReadersController : ControllerBase
	{
		private readonly IReaderService _readerService;

		public ReadersController(IReaderService readerService)
		{
			_readerService = readerService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Reader reader)
		{
			var existingReader = await _readerService.Get(reader.ReaderNumber);

			if (existingReader is not null)
			{
				return Conflict();
			}

			await _readerService.Add(reader);

			return Ok();
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var reader = await _readerService.Get(id);

			if (reader is null)
			{
				return NotFound();
			}

			await _readerService.Delete(id);
			return Ok();
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<Reader>> Get(Guid id)
		{
			var reader = await _readerService.Get(id);

			if (reader is null)
			{
				return NotFound();
			}

			return Ok(reader);
		}

		[HttpGet]
		public async Task<ActionResult<List<Reader>>> GetAll()
		{
			return Ok(await _readerService.GetAll());
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Reader newReader)
		{
			if (id != newReader.ReaderNumber)
			{
				return BadRequest();
			}

			var existingReader = await _readerService.Get(id);

			if (existingReader is null)
			{
				return NotFound();
			}

			await _readerService.Update(newReader);

			return Ok();
		}
	}
}
