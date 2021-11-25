using System.Collections.Generic;

namespace DungeonCrawl.Actors.Static
{
    public class Wall : Actor
    {
        public override int DefaultSpriteId { get; set; } = 825;

        public override string DefaultName => "Wall";

        public Dictionary<string, int> SpriteVariants = new Dictionary<string, int>()
        {
            {"wall", 15}, {"water", 247}, {"tree", 50}, {"necklace", 428}, {"fence", 780}, {"tree1", 99}, {"roof1", 729},
            {"roof2", 730}, {"roof3", 731}, {"wallHouseLeft", 777}, {"wallHouseRight", 779}, {"tombstone", 672}, {"fireplace", 493}, 
            {"woodenObstacle", 251}, {"crown", 139}, {"crossUp", 600}, {"crossDown", 648}, {"door2", 340}, 
            {"stoneWallLeftCorner", 17}, {"stoneWallMiddle", 18}, {"stoneWallRightCorner", 19}, {"stoneWallLeft", 65}, 
            {"stoneWallLeftDownCorner", 113}, {"stoneWallLeftDown", 114}, {"stoneWallRightDownCorner", 115}, {"candle", 724}, {"tower", 1002}, 
            {"redroof1", 580}, {"redroof2", 581}, {"redroof3", 582}, {"redWall", 628}, {"stoneWallRight", 67}, {"candle1", 631}, 
            {"greenLeftUpCorner", 767}, {"greenRightUpCorner", 769}, {"greenLeftDownCorner", 863}, {"greenRightDownCorner", 865},
            {"greenLeft", 815 }, {"greenRight", 817 }, {"closedDoor", 146}, {"greenUp", 768}, {"greenDown", 864}, {"houseDoor", 778}, {"houseWall", 777}, {"houseWall1", 779},{"boat",921}
        };

        public override void SetSprite(Dictionary<string, int> variants, string key)
        {
            base.SetSprite(SpriteVariants, key);
        }
    }
}
