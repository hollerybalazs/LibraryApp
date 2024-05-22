using Library.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BorrowsController : ControllerBase
	{
		private readonly IBorrowService _borrowService;

		public BorrowsController(IBorrowService borrowService)
		{
			_borrowService = borrowService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Borrow borrow)
		{
			var existingBorrow = await _borrowService.Get(borrow.Id);

			if (existingBorrow is not null)
			{
				return Conflict();
			}

			await _borrowService.Add(borrow);

			return Ok();
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var borrow = await _borrowService.Get(id);

			if (borrow is null)
			{
				return NotFound();
			}

			await _borrowService.Delete(id);
			return Ok();
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<Borrow>> Get(Guid id)
		{
			var borrow = await _borrowService.Get(id);

			if (borrow is null)
			{
				return NotFound();
			}

			return Ok(borrow);
		}

		[HttpGet]
		public async Task<ActionResult<List<Borrow>>> GetAll()
		{
			return Ok(await _borrowService.GetAll());
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Borrow newBorrow)
		{
			if (id != newBorrow.Id)
			{
				return BadRequest();
			}

			var existingBorrow = await _borrowService.Get(id);

			if (existingBorrow is null)
			{
				return NotFound();
			}

			await _borrowService.Update(newBorrow);

			return Ok();
		}
	}
}
