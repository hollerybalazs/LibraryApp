using Library.Shared;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
	private readonly IBookService _bookService;

	public BooksController(IBookService bookService)
	{
		_bookService = bookService;
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] Book book)
	{
		var existingBook = await _bookService.Get(book.InventoryNumber);

		if (existingBook is not null)
		{
			return Conflict();
		}

		await _bookService.Add(book);

		return Ok();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var book = await _bookService.Get(id);

		if (book is null)
		{
			return NotFound();
		}

		await _bookService.Delete(id);
		return Ok();
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<Book>> Get(Guid id)
	{
		var book = await _bookService.Get(id);

		if (book is null)
		{
			return NotFound();
		}

		return Ok(book);
	}

	[HttpGet]
	public async Task<ActionResult<List<Book>>> GetAll()
	{
		return Ok(await _bookService.GetAll());
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] Book newBook)
	{
		if (id != newBook.InventoryNumber)
		{
			return BadRequest();
		}

		var existingBook = await _bookService.Get(id);

		if (existingBook is null)
		{
			return NotFound();
		}

		await _bookService.Update(newBook);

		return Ok();
	}
}