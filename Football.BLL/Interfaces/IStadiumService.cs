using Football.BLL.DTO;
using System.Collections.Generic;

namespace Football.BLL.Interfaces
{
    public interface IStadiumService
    {
        void Create(StadiumDTO stadiumDto);
        StadiumDTO GetStadium(int? id);
        IEnumerable<StadiumDTO> GetStadiums();
        void DeleteStadium(int? id);
        void Dispose();
    }
}
