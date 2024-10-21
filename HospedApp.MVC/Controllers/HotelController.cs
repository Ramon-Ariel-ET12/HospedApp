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
    public async Task<IActionResult> ModifyAddress(int id)
    {
        var addresses = await Ado.GetAddresses();
        var hotel = addresses.FirstOrDefault(x => x.IdAddress == id);
        ViewBag.IdHotel = hotel!.Hotel!.IdHotel;
        return View("AddAddress", hotel);
    }
    [HttpGet]
    public IActionResult RegisterAddress(int id)
    {
        ViewBag.IdHotel = id;
        return View("AddAddress", null);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress(Address address)
    {

        if (address.IdAddress == 0)
        {
            await Ado.CreateAddress(address);
        }
        else
        {
            await Ado.ModifyAddress(address);
        }
        return Redirect($"Detail/{address.Hotel!.IdHotel}");
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        var hotel = await Ado.GetHotels();
        var encontrado = hotel.FirstOrDefault(x => x.IdHotel == id);
        encontrado!.Addresses = encontrado.Addresses!.OrderByDescending(x => x.IdAddress).ToList();
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
        var address = await Ado.GetAddresses();
        var encontrado = hotel.Where(x => x.Address!.IdAddress == id);
        if (encontrado != null)
        {
            ViewBag.Domicile = address.FirstOrDefault(x => x.IdAddress == id)!.Domicile;
            ViewBag.IdAddress = id;
            return View(encontrado.OrderByDescending(x => x.Number));
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> SeeRoom(int id)
    {
        HotelRoom encontrado = new();
        encontrado.Address = new();
        var hotelRooms = await Ado.GetHotelRooms();
        ViewBag.Rooms = await Ado.GetRoomBeds();
        ViewBag.IdDirection = id;
        var x = hotelRooms.FirstOrDefault(x => x.Address!.IdAddress == id);
        if (x != null)
        {
            encontrado = x;
        }
        return View("AddRoom", encontrado);
    }
    [HttpPost]
    public async Task<IActionResult> AddRoom(HotelRoom hotelRoom)
    {
        var idhotel = await Ado.GetAddresses();
        hotelRoom.Address!.Hotel = idhotel.FirstOrDefault(x => x.IdAddress == hotelRoom.Address!.IdAddress)!.Hotel!;
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
}
