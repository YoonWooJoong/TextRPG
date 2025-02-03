using System.Numerics;
using System;

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
        public class Item
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

        static void CreateName()
        {
            // 플레이어 이름
            Console.WriteLine("안녕하세요 게임을 시작하기 전 당신의 이름을 알려주세요");
            Console.WriteLine();
            Console.Write(">>");
            string playerName = Console.ReadLine();
            Console.Clear();
        }

        static void SettingPlayerState()
        {
            
        }
        static void Main(string[] args)
        {
            List<Item> makeItem = new List<Item>(); // 아이템 생성
            makeItem.Add(new Item("낡은 검", 2, 0, 600, "쉽게 볼 수 있는 낡은 검 입니다.",1, true, false));
            makeItem.Add(new Item("청동 도끼", 5, 0, 1500, "어디선가 사용됐던거 같은 도끼입니다..", 1, false, false));
            makeItem.Add(new Item("스파르타의 창", 7, 0, 3000, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, false, false));
            makeItem.Add(new Item("수련자 갑옷", 0, 5, 1000, "수련에 도움을 주는 갑옷입니다.", 2, false, false));
            makeItem.Add(new Item("무쇠갑옷", 0, 9, 2000, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 2, false, false));
            makeItem.Add(new Item("스파르타의 갑옷", 0, 15, 3500, "수련에 도움을 주는 갑옷입니다.", 2, false, false));

            List<Item> inven = new List<Item>(); // 인벤토리 생성       
            inven.Add(makeItem[0]);

            CreateName(); // 이름 생성

            // 플레이어 정보 설정
            Player player;
            player.level = 01;
            player.job = "( 전사 )";
            player.attack = 10;
            player.defence = 5;
            player.health = 100;
            player.gold = 1500;

            int firstNum = 0;
            int secondNum = 1;

            int EquipAttackstate = 0; // 장착 공격력 변수
            int EquipDefencetate = 0; // 장착 방어력 변수
            
            bool isTrue = false;
            bool isStartPg = true; //시작 화면 활성화 여부
            bool isInvenPg = false; // 인벤토리 관리 활성화 여부

            
            while (true)
            {
                if (isStartPg)
                {
                    // 게임 시작 화면
                    Console.WriteLine("마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("던전에 들어가기전 행동을 선택해주세요");
                    Console.WriteLine();
                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    isTrue = int.TryParse(Console.ReadLine(), out firstNum);
                }
                if (isTrue && firstNum == 1)
                {
                    //상태보기
                    Console.Clear();
                    Console.WriteLine("[상태 보기]");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                    Console.WriteLine();
                    Console.WriteLine("Lv. {0}", player.level);
                    Console.WriteLine("{0} {1}", playerName, player.job);
                    if (EquipAttackstate == 0) // 장비를 장착해서 공격력을 추가했을때와 안했을때
                    {
                        Console.WriteLine("공격력 : {0}", player.attack);
                    }
                    else if (EquipAttackstate > 0)
                    {
                        Console.WriteLine("공격력 : {0} (+{1})", player.attack ,EquipAttackstate);
                    }
                    if (EquipDefencetate == 0) // 장비를 장착해서 방어력을 추가했을때와 안했을때
                    {
                        Console.WriteLine("방어력 : {0}", player.defence);
                    }
                    else if (EquipDefencetate > 0)
                    {
                        Console.WriteLine("방어력 : {0} (+{1})", player.defence, EquipDefencetate);
                    }
                    Console.WriteLine("체 력 : {0}", player.health);
                    Console.WriteLine("Gold : {0}", player.gold);
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    bool isState = int.TryParse(Console.ReadLine(), out secondNum);
                    if (!isState || secondNum != 0)
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
                else if (isTrue && firstNum == 2)
                {
                    // 인벤토리
                    Console.Clear();
                    Console.WriteLine("[인벤토리]");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < inven.Count; i++)
                    {
                        if (isInvenPg == false)
                        {
                            if ((inven[i].itemType == 1) && (inven[i].isEquip == false) && (inven[i].isOwn == true)) // 무기를 장착중이 아니면서 가지고 있고 관리가 활성화가 아닌경우
                            {
                                Console.WriteLine("- {0}\t| 공격력 +{1} \t| {2}", inven[i].name, inven[i].damage, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 2) && (inven[i].isEquip == false) && (inven[i].isOwn == true)) // 방어구를 장착중이 아니면서 가지고 있고 관리가 활성화가 아닌경우
                            {
                                Console.WriteLine("- {0}\t| 방어력 +{1} \t| {2}", inven[i].name, inven[i].defence, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 무기를 장착중이면서 가지고 있고 관리가 활성화가 아닌경우
                            {
                                Console.WriteLine("- [E]{0}\t| 공격력 +{1} \t| {2}", inven[i].name, inven[i].damage, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 방어구를 장착중이면서 가지고 있고 관리가 활성화가 아닌경우
                            {
                                Console.WriteLine("- [E]{0}\t| 방어력 +{1} \t| {2}", inven[i].name, inven[i].defence, inven[i].explanation);
                            }
                        }
                        else
                        {
                            if ((inven[i].itemType == 1) && (inven[i].isEquip == false) && (inven[i].isOwn == true)) // 무기를 장착중이 아니면서 가지고 있고 관리가 활성화인 경우
                            {
                                Console.WriteLine("- {0} {1}\t| 공격력 +{2} \t| {3}", i+1 ,inven[i].name, inven[i].damage, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 2) && (inven[i].isEquip == false) && (inven[i].isOwn == true)) // 방어구를 장착중이 아니면서 가지고 있고 관리가 활성화인 경우
                            {
                                Console.WriteLine("- {0} {1}\t| 방어력 +{2} \t| {3}", i + 1, inven[i].name, inven[i].defence, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 무기를 장착중이면서 가지고 있고 관리가 활성화인 경우
                            {
                                Console.WriteLine("- [E]{0} {1}\t| 공격력 +{2} \t| {3}", i + 1, inven[i].name, inven[i].damage, inven[i].explanation);
                            }
                            else if ((inven[i].itemType == 1) && (inven[i].isEquip == true) && (inven[i].isOwn == true)) // 방어구를 장착중이면서 가지고 있고 관리가 활성화인 경우
                            {
                                Console.WriteLine("- [E]{0} {1}\t| 방어력 +{2} \t| {3}", i + 1, inven[i].name, inven[i].defence, inven[i].explanation);
                            }
                        }
                    }
                    if (isInvenPg == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1. 장착 관리");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요");
                        Console.Write(">>");
                        bool isState = int.TryParse(Console.ReadLine(), out secondNum);

                        if (isState && secondNum == 1) // 장비 관리로 넘어갈시 초기화면 비활성화와 장비관리 활성화
                        {
                            Console.Clear();
                            isInvenPg = true;
                            isStartPg = false;
                        }
                        else if (!isState || secondNum != 0) // 0이 아닐때 예외처리
                        {
                            Console.WriteLine("입력이 잘못됐습니다.");
                            Thread.Sleep(500); // 0.5초 지연
                            Console.Clear();
                            isStartPg = false;
                        }
                        else // 0 일때 출력
                        {
                            Console.Clear();
                            isStartPg = true;
                        }
                    }
                    else // 장비관리가 활성화 됐을때의 조건문
                    {
                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.WriteLine("원하시는 행동을 입력해주세요");
                        Console.Write(">>");
                        bool isState = int.TryParse(Console.ReadLine(), out secondNum);

                        if (isState && secondNum == 0) // 0입력시 장비관리 비활성화
                        {
                            Console.Clear();
                            isInvenPg = false;
                        }
                        for (int i = 0; i < inven.Count; i++) // 인벤토리갯수 만큼 반복
                        {
                            if (i+1 == secondNum) // i는 0부터 시작해서 +1 해줌으로써 입력값과 같게 해줌
                            {
                                inven[i].isEquip = !inven[i].isEquip; // 장착 장착해제
                                if ((inven[i].itemType == 1) && (inven[i].isEquip == true)) // 무기타입이고 장착중이면 무기 데미지를 더함
                                {
                                    EquipAttackstate += inven[i].damage;
                                }
                                else if (inven[i].itemType == 1 && (inven[i].isEquip == false)) // 무기타입이고 장착해제면 무기 데미지를 뺌
                                {
                                    EquipAttackstate -= inven[i].damage;
                                }
                                else if (inven[i].itemType == 2 && (inven[i].isEquip == true)) // 방어타입이고 장착중이면 방어력을 더함
                                {
                                    EquipDefencetate += inven[i].defence;
                                }
                                else if (inven[i].itemType == 2 && (inven[i].isEquip == false)) // 방어타입이고 장착해제면 방어력을 뺌
                                {
                                    EquipDefencetate -= inven[i].defence;
                                }
                            }
                        }
                    }
                }
                else if (isTrue && firstNum == 3)
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
