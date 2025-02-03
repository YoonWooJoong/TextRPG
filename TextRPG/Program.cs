namespace TextRPG
{
    internal class Program
    {
        enum ItemName
        {
            basicArmor = 0,
            ironArmor = 1,
            spartaArmor = 2,
            oldSword = 3,
            bronzeAxe = 4,
            spartaSpear =5
        }
        struct Player // 플레이어 정보를 구조체 사용
        {
            public int level;
            public string job;
            public int attack;
            public int defence;
            public int health;
            public int gold;
        }
        struct Item
        {
            public string name;
            public int damage;
            public int defence;
            public int gold;
            public string explanation;
            public int itemType; // 아이템이 공격아이템인지, 방어아이템인지 공격이면 1 방어면 2
            public bool isOwn; // 소유중인지 아닌지
            public bool isEquip; // 장착중인지 아닌지
            public Item(string _name, int _damage, int _defence, int _gold, string _explanation, int _itemType, bool _isOwn, bool _isEquip)
            {
                name = _name;
                damage = _damage;
                defence = _defence;
                gold = _gold;
                explanation = _explanation;
                itemType = _itemType;
                isOwn = _isOwn;
                isEquip = _isEquip;
            }
        }

        static void Main(string[] args)
        {
            List<Item> makeItem = new List<Item>(); // 아이템 생성
            makeItem.Add(new Item("낡은 검", 2, 0, 600, "쉽게 볼 수 있는 낡은 검 입니다.",1, false, false));
            makeItem.Add(new Item("청동 도끼", 5, 0, 1500, "어디선가 사용됐던거 같은 도끼입니다..", 1, false, false));
            makeItem.Add(new Item("스파르타의 창", 7, 0, 3000, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, false, false));
            makeItem.Add(new Item("수련자 갑옷", 0, 5, 1000, "수련에 도움을 주는 갑옷입니다.", 2, false, false));
            makeItem.Add(new Item("무쇠갑옷", 0, 9, 2000, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 2, false, false));
            makeItem.Add(new Item("스파르타의 갑옷", 0, 15, 3500, "수련에 도움을 주는 갑옷입니다.", 2, false, false));

            List<Item> inven = new List<Item>(); // 인벤토리 생성       
            inven.Add(makeItem[0]);

            // 플레이어 이름
            Console.WriteLine("안녕하세요 게임을 시작하기 전 당신의 이름을 알려주세요");
            Console.WriteLine();
            Console.Write(">>");
            string playerName = Console.ReadLine();
            Console.Clear();

            // 플레이어 정보 설정
            Player player;
            player.level = 01;
            player.job = "( 전사 )";
            player.attack = 10;
            player.defence = 5;
            player.health = 100;
            player.gold = 1500;

            int outNum = 0;
            int inNum = 1;
            bool isTrue = false;
            bool isStartPg = true;
            // 게임 시작 화면
            while (true)
            {
                if (isStartPg)
                {
                    Console.WriteLine("마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("던전에 들어가기전 행동을 선택해주세요");
                    Console.WriteLine();
                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    isTrue = int.TryParse(Console.ReadLine(), out outNum);
                }
                if (isTrue && outNum == 1)
                {
                    Console.Clear();
                    //상태보기
                    Console.WriteLine("[상태 보기]");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                    Console.WriteLine();
                    Console.WriteLine("Lv. {0}", player.level);
                    Console.WriteLine("{0} {1}", playerName, player.job);
                    Console.WriteLine("공격력 : {0}", player.attack);
                    Console.WriteLine("방어력 : {0}", player.defence);
                    Console.WriteLine("체 력 : {0}", player.health);
                    Console.WriteLine("Gold : {0}", player.gold);
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    bool isState = int.TryParse(Console.ReadLine(), out inNum);
                    if (!isState || inNum != 0)
                    {
                        Console.WriteLine("입력이 잘못됐습니다.");
                        Thread.Sleep(500); // 0.5초 지연
                        Console.Clear();
                        isStartPg = false;
                    }
                    else
                    {
                        Console.Clear();
                        isStartPg = true;
                    }
                }
                else if (isTrue && outNum == 2)
                {
                    // 인벤토리
                    Console.Clear();
                    //상태보기
                    Console.WriteLine("[인벤토리]");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < inven.Count; i++)
                    {
                        if ((inven[i].itemType == 1) &&(inven[i].isEquip == false) && (inven[i].isOwn == true)) // 무기를 장착중이 아니면서 가지고 있다면
                        {
                            Console.WriteLine("- {0}\t| 공격력 +{1} \t| {2}", inven[i].name, inven[i].damage, inven[i].explanation);
                        }
                        else if ((inven[i].itemType == 2) && (inven[i].isEquip == false) && (inven[i].isOwn == true)) // 방어구를 장착중이 아니면서 가지고 있다면
                        {
                            Console.WriteLine("- {0}\t| 방어력 +{1} \t| {2}", inven[i].name, inven[i].defence, inven[i].explanation);
                        }
                        else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 무기를 장착중이면서 가지고 있다면
                        {
                            Console.WriteLine("- [E]{0}\t| 공격력 +{1} \t| {2}", inven[i].name, inven[i].damage, inven[i].explanation);
                        }
                        else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 방어구를 장착중이면서 가지고 있다면
                        {
                            Console.WriteLine("- [E]{0}\t| 방어력 +{1} \t| {2}", inven[i].name, inven[i].defence, inven[i].explanation);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("1. 장착 관리");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    bool isState = int.TryParse(Console.ReadLine(), out inNum);
                    if (!isState || inNum != 0)
                    {
                        Console.WriteLine("입력이 잘못됐습니다.");
                        Thread.Sleep(500); // 0.5초 지연
                        Console.Clear();
                        isStartPg = false;
                    }
                    else
                    {
                        Console.Clear();
                        isStartPg = true;
                    }
                }
                else if (isTrue && outNum == 3)
                {
                    // 상점
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500); // 0.5초 지연
                    Console.Clear();
                }
            }

        }
    }
}
