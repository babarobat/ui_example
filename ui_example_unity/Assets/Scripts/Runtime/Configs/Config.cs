using System.Collections.Generic;

namespace Runtime.Configs
{
    public class Config
    {
        public Loaclization Localization = new();

        public List<Avatar> Avatars = new()
        {
            new Avatar { Id = "avatar_bunny", AssetPath = "Avatars/avatar_bunny", Name = "Bunny" , SelectionPrice = 3},
            new Avatar { Id = "avatar_ninja", AssetPath = "Avatars/avatar_ninja", Name = "Ninja", SelectionPrice = 7 },
            new Avatar { Id = "avatar_elephant", AssetPath = "Avatars/avatar_elephant", Name = "Elephant", SelectionPrice = 5},
            new Avatar { Id = "avatar_ex", AssetPath = "Avatars/avatar_ex", Name = "Lady", SelectionPrice = 4 },
            new Avatar { Id = "avatar_gay_2", AssetPath = "Avatars/avatar_gay_2", Name = "Abstractus", SelectionPrice = 6 },
            new Avatar { Id = "avatar_gay", AssetPath = "Avatars/avatar_gay", Name = "Sailor", SelectionPrice = 1 },
            new Avatar { Id = "avatar_penguin", AssetPath = "Avatars/avatar_penguin", Name = "Penguin", SelectionPrice = 3 },
        };
    }
}