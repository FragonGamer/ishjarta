public class GridPosdataType
{
    public int xPos;
    public int yPos;
    public int roomId;
    public bool hasSDoor;
    public bool hasNDoor;
    public bool hasEDoor;
    public bool hasWDoor;

    public GridPosdataType()
    {

    }
    public GridPosdataType(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.roomId = -1;
        this.hasSDoor = false;
        this.hasNDoor = false;
        this.hasWDoor = false;
        this.hasEDoor = false;
    }
}
