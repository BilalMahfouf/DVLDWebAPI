using AutoMapper;
using BusinessLoginLayer.Services;
using Core.DTOs.Country;
using Core.DTOs.Person;

using Core.Interfaces.Repositories.People;
using DataAccessLayer;
using Moq;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class PersonServiceTests
    {
        IEnumerable<Person> people = new List<Person>
{
    new Person
    {
        PersonID = 1,
        NationalNo = "12345678901234567890",
        FirstName = "Ali",
        SecondName = "Ahmed",
        ThirdName = "Hassan",
        LastName = "Farouk",
        DateOfBirth = new DateTime(1995, 3, 15),
        Gender = 1, // Male
        Address = "123 Main Street, Algiers, Algeria",
        Phone = "0550123456",
        Email = "ali.farouk@example.com",
        NationalityCountryID = 1,
        ImagePath = "images/ali.jpg"
    },
    new Person
    {
        PersonID = 2,
        NationalNo = "98765432109876543210",
        FirstName = "Sara",
        SecondName = "Mohamed",
        ThirdName = "Amine",
        LastName = "Zaki",
        DateOfBirth = new DateTime(2000, 6, 24),
        Gender = 0, // Female
        Address = "456 Elm Street, Oran, Algeria",
        Phone = "0771987654",
        Email = "sara.zaki@example.com",
        NationalityCountryID = 2,
        ImagePath = "images/sara.jpg"
    },
    new Person
    {
        PersonID = 3,
        NationalNo = "11112222333344445555",
        FirstName = "Khaled",
        SecondName = "Youssef",
        ThirdName = "Tariq",
        LastName = "Mahmoud",
        DateOfBirth = new DateTime(1988, 12, 1),
        Gender = 1,
        Address = "789 Cedar Avenue, Constantine, Algeria",
        Phone = "0660345621",
        Email = "khaled.mahmoud@example.com",
        NationalityCountryID = 1,
        ImagePath = "images/khaled.png"
    },
    new Person
    {
        PersonID = 4,
        NationalNo = "22223333444455556666",
        FirstName = "Layla",
        SecondName = "Fouad",
        ThirdName = "Nour",
        LastName = "El Din",
        DateOfBirth = new DateTime(1993, 9, 8),
        Gender = 0,
        Address = "321 Palm Road, Tlemcen, Algeria",
        Phone = "0560896543",
        Email = "layla.eldin@example.com",
        NationalityCountryID = 3,
        ImagePath = "images/layla.jpg"
    },
    new Person
    {
        PersonID = 5,
        NationalNo = "33334444555566667777",
        FirstName = "Yassine",
        SecondName = "Samir",
        ThirdName = "Karim",
        LastName = "Belkacem",
        DateOfBirth = new DateTime(1990, 2, 20),
        Gender = 1,
        Address = "654 Oak Lane, Annaba, Algeria",
        Phone = "0790456123",
        Email = "yassine.belkacem@example.com",
        NationalityCountryID = 4,
        ImagePath = "images/yassine.jpg"
    }
};

        IEnumerable<ReadPersonDTO> readPeople = new List<ReadPersonDTO>
{
    new ReadPersonDTO(
        personID: 1,
        nationalNo: "12345678901234567890",
        firstName: "Ali",
        secondName: "Ahmed",
        thirdName: "Hassan",
        lastName: "Farouk",
        dateOfBirth: new DateTime(1995, 3, 15),
        gender: 0,
        address: "123 Main Street, Algiers",
        phone: "0550123456",
        email: "ali.farouk@example.com",
        nationalityCountryID: 1,
        imagePath: "images/ali.jpg",
        country: new ReadCountryDTO
        {
            CountryID = 1,
            CountryName = "Algeria"
        }
    ),
    new ReadPersonDTO(
        personID: 2,
        nationalNo: "98765432109876543210",
        firstName: "Sara",
        secondName: "Mohamed",
        thirdName: "Amine",
        lastName: "Zaki",
        dateOfBirth: new DateTime(2000, 6, 24),
        gender: 1,
        address: "456 Elm Street, Oran",
        phone: "0771987654",
        email: "sara.zaki@example.com",
        nationalityCountryID: 2,
        imagePath: "images/sara.jpg",
        country: new ReadCountryDTO
        {
            CountryID = 2,
            CountryName = "Morocco"
        }
    ),
    new ReadPersonDTO(
        personID: 3,
        nationalNo: "11112222333344445555",
        firstName: "Khaled",
        secondName: "Youssef",
        thirdName: "Tariq",
        lastName: "Mahmoud",
        dateOfBirth: new DateTime(1988, 12, 1),
        gender: 0,
        address: "789 Cedar Avenue, Constantine",
        phone: "0660345621",
        email: "khaled.mahmoud@example.com",
        nationalityCountryID: 3,
        imagePath: "images/khaled.png",
        country: new ReadCountryDTO
        {
            CountryID = 3,
            CountryName = "Tunisia"
        }
    ),
    new ReadPersonDTO(
        personID: 4,
        nationalNo: "22223333444455556666",
        firstName: "Layla",
        secondName: "Fouad",
        thirdName: "Nour",
        lastName: "El Din",
        dateOfBirth: new DateTime(1993, 9, 8),
        gender: 1,
        address: "321 Palm Road, Tlemcen",
        phone: "0560896543",
        email: "layla.eldin@example.com",
        nationalityCountryID: 4,
        imagePath: "images/layla.jpg",
        country: new ReadCountryDTO
        {
            CountryID = 4,
            CountryName = "Egypt"
        }
    ),
    new ReadPersonDTO(
        personID: 5,
        nationalNo: "33334444555566667777",
        firstName: "Yassine",
        secondName: "Samir",
        thirdName: "Karim",
        lastName: "Belkacem",
        dateOfBirth: new DateTime(1990, 2, 20),
        gender: 0,
        address: "654 Oak Lane, Annaba",
        phone: "0790456123",
        email: "yassine.belkacem@example.com",
        nationalityCountryID: 5,
        imagePath: "images/yassine.jpg",
        country: new ReadCountryDTO
        {
            CountryID = 5,
            CountryName = "Libya"
        }
    )
};
        [Fact]
        public async Task GetAllAsync_NoInput_ShouldReturnAllPeople()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(people);
            mapper.Setup(m => m.Map<IEnumerable<ReadPersonDTO>>(people)).Returns(readPeople);
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
            Assert.Equal(readPeople, result);
        }

        [Fact]
        public async Task GetAllAsync_NoInput_ReturnEnumerableEmpty()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Person>());
            mapper.Setup(m => m.Map<IEnumerable<ReadPersonDTO>>(It.IsAny<IEnumerable<Person>>())).Returns(new List<ReadPersonDTO>());
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.GetAllAsync();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreatePersonAsync_PersonDTOInput_ReturnInsertedID()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            var dto = new PersonDTO { NationalNo = "123", FirstName = "Ali" };
            var entity = new Person { PersonID = 0, NationalNo = "123", FirstName = "Ali" };
            repo.Setup(r => r.AddAsync(entity)).ReturnsAsync(6);
            mapper.Setup(m => m.Map<Person>(dto)).Returns(entity);
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.CreatePersonAsync(dto);
            Assert.Equal(6, result);
        }

        [Fact]
        public async Task CreatePersonAsync_NullPersonDTO_ThrowsArgumentNullException()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            var service = new PersonService(repo.Object, mapper.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreatePersonAsync(null));
        }

        [Fact]
        public async Task DeletePersonAsync_idInput_ReturnTrue()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.DeleteAsync(2)).ReturnsAsync(true);
            var service = new PersonService(repo.Object, null);
            var result = await service.DeletePersonAsync(2);
            Assert.True(result);
        }

        [Fact]
        public async Task DeletePersonAsync_idInput_ReturnFalse()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.DeleteAsync(50)).ReturnsAsync(false);
            var service = new PersonService(repo.Object, null);
            var result = await service.DeletePersonAsync(50);
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePersonAsync_InvalidInput_ThrowArgumentOutOfRangeException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, null);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.DeletePersonAsync(0));
        }

        [Fact]
        public async Task FindAsync_WithValidId_ReturnsReadPersonDTO()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            var person = new Person { PersonID = 1 };
            var dto = new ReadPersonDTO { PersonID = 1 };
            repo.Setup(r => r.FindAsync(1)).ReturnsAsync(person);
            mapper.Setup(m => m.Map<ReadPersonDTO>(person)).Returns(dto);
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.PersonID);
        }

        [Fact]
        public async Task FindAsync_WithZeroId_ThrowsArgumentOutOfRangeException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, new Mock<IMapper>().Object);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.FindAsync(0));
        }

        [Fact]
        public async Task FindAsync_WithNonExistingId_ReturnsNull()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.FindAsync(10)).ReturnsAsync((Person)null);
            var service = new PersonService(repo.Object, new Mock<IMapper>().Object);
            var result = await service.FindAsync(10);
            Assert.Null(result);
        }

        [Fact]
        public async Task FindAsync_ByNationalNo_ReturnsPersonDTO()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            var person = new Person { NationalNo = "ABC123" };
            var dto = new PersonDTO { NationalNo = "ABC123" };
            repo.Setup(r => r.FindByNationalNoAsync("ABC123")).ReturnsAsync(person);
            mapper.Setup(m => m.Map<PersonDTO>(person)).Returns(dto);
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.FindAsync("ABC123");
            Assert.NotNull(result);
            Assert.Equal("ABC123", result.NationalNo);
        }

        [Fact]
        public async Task FindAsync_ByNullNationalNo_ThrowsArgumentNullException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, new Mock<IMapper>().Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.FindAsync(" "));
        }

        [Fact]
        public async Task FindAsync_ByNonExistingNationalNo_ReturnsNull()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.FindByNationalNoAsync("XYZ")).ReturnsAsync((Person)null);
            var service = new PersonService(repo.Object, new Mock<IMapper>().Object);
            var result = await service.FindAsync("XYZ");
            Assert.Null(result);
        }

        [Fact]
        public async Task IsExistAsync_ById_ReturnsTrue()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.IsExistAsync(1)).ReturnsAsync(true);
            var service = new PersonService(repo.Object, null);
            var result = await service.IsExistAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistAsync_ByInvalidId_ThrowsArgumentOutOfRangeException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, null);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.IsExistAsync(0));
        }

        [Fact]
        public async Task IsExistAsync_ByNationalNo_ReturnsTrue()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.IsExistAsync("A1")).ReturnsAsync(true);
            var service = new PersonService(repo.Object, null);
            var result = await service.IsExistAsync("A1");
            Assert.True(result);
        }

        [Fact]
        public async Task IsExistAsync_ByEmptyNationalNo_ThrowsArgumentNullException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, null);
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.IsExistAsync(" "));
        }

        [Fact]
        public async Task UpdatePersonAsync_ValidInput_UpdatesSuccessfully()
        {
            var repo = new Mock<IPersonRepository>();
            var mapper = new Mock<IMapper>();
            var dto = new PersonDTO { FirstName = "Ali" };
            var person = new Person { PersonID = 1 };
            repo.Setup(r => r.FindAsync(1)).ReturnsAsync(person);
            repo.Setup(r => r.UpdateAsync(person)).ReturnsAsync(true);
            mapper.Setup(m => m.Map(dto, person));
            var service = new PersonService(repo.Object, mapper.Object);
            var result = await service.UpdatePersonAsync(1, dto);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdatePersonAsync_NullDTO_ThrowsArgumentNullException()
        {
            var service = new PersonService(new Mock<IPersonRepository>().Object, new Mock<IMapper>().Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdatePersonAsync(1, null));
        }

        [Fact]
        public async Task UpdatePersonAsync_NotFound_ThrowsException()
        {
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.FindAsync(2)).ReturnsAsync((Person)null);
            var service = new PersonService(repo.Object, new Mock<IMapper>().Object);
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdatePersonAsync(2, new PersonDTO()));
        }
    }
}



   