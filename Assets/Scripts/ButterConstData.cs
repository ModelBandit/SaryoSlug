/*
 * 버터 스프라이트 데이터
 * 
 * 스프라이트 갯수 40개
 * 오더 역순으로 버텍스 배치
 * 
 * 해당 정보를 서브메쉬로 넣어서 외곽라인 만들것
 * 
 * */
class ButterConstData
{
    private const int Ear_R = 25;
    private const int Ahoge = 37;
    private const int Back_Hair = 67;
    private const int Tail = 93;
    private const int Hair_5 = 132;
    private const int Neck = 136;
    private const int Leg_Joint_R = 140;
    private const int Leg_Calf_R = 172;
    private const int Leg_Thigh_R = 178;
    private const int Leg_Joint_L = 181;
    private const int Leg_Calf_L = 212;
    private const int Leg_Thigh_L = 218;
    private const int Arm_Joint_R = 222;
    private const int Arm_Forearm_R = 228;
    private const int Arm_Upper_R = 238;
    private const int Body = 283;
    private const int Head = 324;
    private const int Cloth = 376;
    private const int Ribbon_Body = 412;
    private const int Sidebag_Ear_L = 426;
    private const int Sidebag = 471;
    private const int Sidebag_Ear_R = 485;
    private const int Flush_L = 490;
    private const int Flush_R = 494;
    private const int Hair_3 = 508;
    private const int Hair_4 = 520;
    private const int Hair_2 = 538;
    private const int Hair_1 = 578;
    private const int Face = 582;
    private const int Ear_L = 606;
    private const int Bell = 633;
    private const int Ribbon_L = 669;
    private const int Ribbon_Head = 703;
    private const int Gun = 735;
    private const int Arm_Joint_L = 738;
    private const int Arm_Forearm_L = 744;
    private const int Arm_Upper_L = 754;

    public int Count = 37;
    public int this[int index]
    {
        get => GetValue(index);
    }

    public int GetValue(int key)
    {
        switch (key)
        {
            case 0:
                return Ear_R;
            case 1:
                return Ahoge;
            case 2:
                return Back_Hair;
            case 3:
                return Tail;
            case 4:
                return Hair_5;
            case 5:
                return Neck;
            case 6:
                return Leg_Joint_R;
            case 7:
                return Leg_Calf_R;
            case 8:
                return Leg_Thigh_R;
            case 9:
                return Leg_Joint_L;
            case 10:
                return Leg_Calf_L;
            case 11:
                return Leg_Thigh_L;
            case 12:
                return Arm_Joint_R;
            case 13:
                return Arm_Forearm_R;
            case 14:
                return Arm_Upper_R;
            case 15:
                return Body;
            case 16:
                return Head;
            case 17:
                return Cloth;
            case 18:
                return Ribbon_Body;
            case 19:
                return Sidebag_Ear_L;
            case 20:
                return Sidebag;
            case 21:
                return Sidebag_Ear_R;
            case 22:
                return Flush_L;
            case 23:
                return Flush_R;
            case 24:
                return Hair_3;
            case 25:
                return Hair_4;
            case 26:
                return Hair_2;
            case 27:
                return Hair_1;
            case 28:
                return Face;
            case 29:
                return Ear_L;
            case 30:
                return Bell;
            case 31:
                return Ribbon_L;
            case 32:
                return Ribbon_Head;
            case 33:
                return Gun;
            case 34:
                return Arm_Joint_L;
            case 35:
                return Arm_Forearm_L;
            case 36:
                return Arm_Upper_L;
            default:
                return 0;
        }
    }
}
