using AutoMapper;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Application.Quotations.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Exceptions;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Customers.Services;

public interface IAddressService
{
    Task<AddressDto> GetAddress(
        long id);

    Task<IEnumerable<AddressDto>> GetCustomerAddresses(long customerId);

    Task<AddressDto> CreateAddress(
        AddressDto addressDto);

    Task<AddressDto> UpdateAddress(
        long id,
        AddressDto addressDto);

    Task DeleteAddress(
        long id);

    Task<AddressDto> SetMainAddress(
        long id);
}

public class AddressService : IAddressService
{
    private readonly IMapper mapper;
    private readonly IRepository<ContactAddress> contactAddressRepository;
    private readonly IRepository<Job> jobRepository;
    private readonly IColliniDbContext dbContext;

    public AddressService(
        IMapper mapper,
        IRepository<ContactAddress> contactAddressRepository,
        IColliniDbContext dbContext, IRepository<Job> jobRepository)
    {
        this.mapper = mapper;
        this.contactAddressRepository = contactAddressRepository;
        this.dbContext = dbContext;
        this.jobRepository = jobRepository;
    }

    public async Task<AddressDto> GetAddress(
        long id)
    {
        var address = await contactAddressRepository.Get(id);

        if (address == null)
        {
            throw new NotFoundException(typeof(ContactAddress), id);
        }

        return address.MapTo<AddressDto>(mapper);
    }

    public async Task<IEnumerable<AddressDto>> GetCustomerAddresses(
        long contactId)
    {

        var addresses = contactAddressRepository
            .Query()
            .Where(x => x.ContactId == contactId)
            .ToArray(); 
      
        if (addresses == null)
        {
            throw new NotFoundException(typeof(ContactAddress), contactId);
        }

        return addresses.MapTo<IEnumerable<AddressDto>>(mapper);
    }

    public async Task<AddressDto> CreateAddress(
        AddressDto addressDto)
    {
        var address = addressDto.MapTo<ContactAddress>(mapper);

        if (address.IsMainAddress)
            ResetAddresses(address.ContactId, null);
        
        await contactAddressRepository.Insert(address);

        await dbContext.SaveChanges();

        return address.MapTo<AddressDto>(mapper);
    }

    public async Task<AddressDto> UpdateAddress(
        long id,
        AddressDto addressDto)
    {
        var address = await contactAddressRepository.Get(id);

        if (address == null)
        {
            throw new NotFoundException(typeof(ContactAddress), id);
        }

        var wasMainAddress = address.IsMainAddress;

        addressDto.MapTo(address, mapper);

        if (address.IsMainAddress)
        {
            ResetAddresses(address.ContactId, id);
        }
        else if (wasMainAddress)
        {
            var firstAddress = await contactAddressRepository.Query()
                .FirstOrDefaultAsync(x => x.ContactId == address.ContactId && x.Id != address.Id);

            if (firstAddress != null)
            {
                firstAddress.IsMainAddress = true;
                contactAddressRepository.Update(firstAddress);
            }
            else
            {
                address.IsMainAddress = true;
            }
        }

        contactAddressRepository.Update(address);

        await dbContext.SaveChanges();

        return address.MapTo<AddressDto>(mapper);
    }

    public async Task DeleteAddress(
        long id)
    {
        var address = await contactAddressRepository.Get(id);

        if (address == null)
        {
            throw new NotFoundException(typeof(ContactAddress), id);
        }

        if (address.IsMainAddress)
        {
            throw new ColliniException("Non puoi eliminare l'indirizzo principale");
        }
        var jobs= await jobRepository
            .Query()
            .AsNoTracking()
            .Where(x => x.CustomerAddressId == id)
            .ToListAsync();

        if (jobs.Any())
        {
            throw new ColliniException("Non puoi eliminare questo indirizzo perchè ha richieste collegate");
        }

        contactAddressRepository.Delete(address);

        await dbContext.SaveChanges();
    }

    public async Task<AddressDto> SetMainAddress(
        long id)
    {
        var address = await contactAddressRepository.Get(id);

        if (address == null)
        {
            throw new NotFoundException(typeof(ContactAddress), id);
        }

        ResetAddresses(address.ContactId, address.Id);

        address.IsMainAddress = true;

        contactAddressRepository.Update(address);

        await dbContext.SaveChanges();

        return address.MapTo<AddressDto>(mapper);
    }
    
    private void ResetAddresses(
        long contactId,
        long? addressId)
    {
        var addresses = contactAddressRepository
            .Query()
            .Where(x => x.ContactId == contactId && x.Id != addressId)
            .ToArray();

        foreach (var a in addresses)
        {
            a.IsMainAddress = false;
            contactAddressRepository.Update(a);
        }
    }
}