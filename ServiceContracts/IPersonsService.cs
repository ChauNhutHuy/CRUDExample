using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a person into the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns></returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// returns a list of all persons
        /// </summary>
        /// <returns>returns a list of objects</returns>
        List<PersonResponse> GetAllPersons();

       PersonResponse GetPersonByPersonID(Guid? personID);
    }
}
