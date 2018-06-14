﻿using Football.BLL.DTO;
using System.Collections.Generic;

namespace Football.BLL.Interfaces
{
    public interface IPlayerService
    {
        void Create(PlayerDTO playerDto);
        PlayerDTO GetPlayer(int? id);
        IEnumerable<PlayerDTO> GetPlayers();
        void DeletePlayer(int? id);
        void UpdatePlayer(PlayerDTO playerDto);
        void Dispose();
    }
}
