using AutoMapper;
using Collini.GestioneInterventi.Application.Customers.DTOs;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Registry;
using Collini.GestioneInterventi.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Collini.GestioneInterventi.Application.Customers.Services;

public interface IContactService
{
    Task<ContactDto> CreateContact(
        ContactDto dto);

    Task DeleteContact(
        long id);

    Task<ContactDto> UpdateContact(
        long id,
        ContactDto dto);

    Task<ContactReadModel> GetContact(
        long id);

    Task<IEnumerable<ContactReadModel>> GetContacts(
        ContactType type);
}

public class ContactService : IContactService
{
    private readonly IMapper mapper;
    private readonly IRepository<Contact> contactRepository;
    private readonly IColliniDbContext dbContext;

    public ContactService(
        IMapper mapper,
        IRepository<Contact> contactRepository,
        IColliniDbContext dbContext)
    {
        this.mapper = mapper;
        this.contactRepository = contactRepository;
        this.dbContext = dbContext;
    }

    public async Task<ContactDto> CreateContact(
        ContactDto dto)
    {
        if (dto.Id > 0)
            throw new ColliniException("Impossibile creare un nuovo contatto con un id già esistente");

        var contact = dto.MapTo<Contact>(mapper);
        await contactRepository.Insert(contact);
        await dbContext.SaveChanges();

        return contact.MapTo<ContactDto>(mapper);
    }

    public async Task DeleteContact(
        long id)
    {
        if (id == 0)
            throw new ColliniException("Impossible eliminare un contatto con id 0");

        var contact = await contactRepository
            .Query()
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        if (contact == null)
            throw new ColliniException($"Impossibile trovare il contatto con id {id}");

        contactRepository.Delete(contact);
        await dbContext.SaveChanges();
    }

    public async Task<ContactDto> UpdateContact(
        long id,
        ContactDto dto)
    {
        if (id == 0)
            throw new ColliniException("Impossibile aggiornare un contatto con id 0");

        var contact = await contactRepository
            .Query()
            //.Include(x => x.Orders)
            //.Include(x=>x.Jobs)
            //.Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        if (contact == null)
            throw new ColliniException($"Impossibile trovare il contatto con id {id}");

        dto.MapTo(contact, mapper);
        contactRepository.Update(contact);
        await dbContext.SaveChanges();

        return contact.MapTo<ContactDto>(mapper);
    }

    public async Task<ContactReadModel> GetContact(
        long id)
    {
        if (id == 0)
            throw new ColliniException("Impossibile recuperare un contatto con id 0");

        var contact = await contactRepository
            .Query()
            .AsNoTracking()
            .Include(x => x.Addresses)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        if (contact == null)
            throw new ColliniException($"Impossibile trovare il contatto con id {id}");

        return contact.MapTo<ContactReadModel>(mapper);
    }

    public async Task<IEnumerable<ContactReadModel>> GetContacts(
        ContactType type)
    {
        var contacts = await contactRepository
            .Query()
            .AsNoTracking()
            .Include(x => x.Addresses)
            .Where(x => x.Type == type)
            .OrderBy(x => x.CompanyName ?? x.Surname)
            .ToArrayAsync();

        return contacts.MapTo<IEnumerable<ContactReadModel>>(mapper);
    }
}