using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.Core.Entities.Relations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class HotelController : Controller
{
    private readonly IAdo Ado;
    public HotelController(IAdo ado) => Ado = ado;

    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var hotel = await Ado.GetHotels();
        return View(hotel.OrderByDescending(x => x.IdHotel));
    }

    [HttpGet]
    public IActionResult Register(Hotel hotel) => View("Upsert", hotel);
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var hotel = await Ado.GetHotels();
        var encontrado = hotel.FirstOrDefault(x => x.IdHotel == id);
        if (encontrado != null)
        {
            return View(encontrado);
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> DetailAddress(int id)
    {
        var hotel = await Ado.GetHotelRooms();
        var encontrado = hotel.Where(x => x.Address!.IdAddress == id);
        ViewBag.Domicile = encontrado.FirstOrDefault()!.Address!.Domicile;
        ViewBag.IdAddress = encontrado.FirstOrDefault()!.Address!.IdAddress;
        if (encontrado != null)
        {
            return View(encontrado.OrderByDescending(x => x.Number));
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> SeeRoom(int id)
    {
        var hotelRooms = await Ado.GetHotelRooms();
        ViewBag.Rooms = await Ado.GetRooms();
        var encontrado = hotelRooms.FirstOrDefault(x => x.Address!.IdAddress == id);
        return View("AddRoom", encontrado);
    }
    [HttpPost]
    public async Task<IActionResult> AddRoom(HotelRoom hotelRoom)
    {
        hotelRoom.Hotel = new();
        var idhotel = await Ado.GetAddresses();
        hotelRoom.Hotel.IdHotel = idhotel.FirstOrDefault(x => x.IdAddress == hotelRoom.Address!.IdAddress)!.Hotel!.IdHotel;
        if (hotelRoom == null)
            return NotFound();
        
        await Ado.CreateHotelRoom(hotelRoom);
        return Redirect($"DetailAddress/{hotelRoom.Address!.IdAddress}");
    }
    [HttpGet]
    public async Task<IActionResult> Modify(int id)
    {
        var hotels = await Ado.GetHotels();
        var hotel = hotels.FirstOrDefault(x => x.IdHotel == id);
        return View("Upsert", hotel);
    }

    [HttpGet]
    public async Task<IActionResult> ModifyAddress(int id)
    {
        var addresses = await Ado.GetAddresses();
        var hotel = addresses.FirstOrDefault(x => x.IdAddress == id);
        return View("UpsertAddress", hotel);
    }
    [HttpPost]
    public IActionResult RegisterAddress(Address address) => View("UpsertAddress", address);

    [HttpPost]
    public async Task<IActionResult> Upsert(Hotel hotel)
    {
        if (hotel == null)
            return RedirectToAction("Hotel");

        if (hotel.IdHotel == 0)
        {
            await Ado.CreateHotel(hotel);
            return RedirectToAction("Index");
        }
        var hotelsearch = await Ado.GetHotels();
        var encontrado = hotelsearch.FirstOrDefault(x => x.IdHotel == hotel.IdHotel);
        if (encontrado != null)
        {
            await Ado.ModifyHotel(hotel);
            return RedirectToAction("Index");
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> UpsertAddress(Address address)
    {
        address.Hotel = new();
        var addresssearch = await Ado.GetAddresses();
        var encontrado = addresssearch.FirstOrDefault(x => x.IdAddress == address.IdAddress);
        var idhotel = encontrado!.Hotel!.IdHotel;
        address!.Hotel!.IdHotel = idhotel;
        if (address == null)
            return RedirectToAction("Hotel");

        if (address.IdAddress == 0)
        {
            await Ado.CreateAddress(address);
            return Redirect($"Detail/{idhotel}");
        }
        if (encontrado != null)
        {
            await Ado.ModifyAddress(address);
            return Redirect($"Detail/{idhotel}");
        }
        return NotFound();
    }
}
