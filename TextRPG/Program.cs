using System.Numerics;
using System;

namespace TextRPG
{
    internal class Program
    {
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
            public string name; //아이템 이름
            public int damage; // 공격력
            public int defence; // 방어력
            public int gold; // 재화
            public string explanation; // 아이템 설명
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

        static string CreateName()
        {
            // 플레이어 이름
            Console.WriteLine("안녕하세요 게임을 시작하기 전 당신의 이름을 알려주세요");
            Console.WriteLine();
            Console.Write(">>");
            string playerName = Console.ReadLine();
            Console.Clear();

            return playerName;
        } // 이름생성

        static void SettingPlayerState(out Player player)
        {
            // 플레이어 정보 설정
            player.level = 01;
            player.job = "( 전사 )";
            player.attack = 10;
            player.defence = 5;
            player.health = 100;
            player.gold = 1500;
        } // 플레이어 능력치 설정하고 유지하기 위해 out을 사용

        static bool FirstScene(bool isTrue, out int firstNum)
        {
            Console.Clear();
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

            return isTrue;
        } // 게임 시작 화면 // isTrue는 return으로 받고 firstNum은 값이 계속해서 바꿔야하기때문에 out 사용

        static void StateScene(Player player, string playerName, ref int equipAttackstate, ref int equipDefenceState, ref int secondNum, ref bool isStartPg)
        {
            //상태보기
            Console.Clear();
            Console.WriteLine("[상태 보기]");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("Lv. {0}", player.level);
            Console.WriteLine("{0} {1}", playerName, player.job);
            if (equipAttackstate == 0) // 장비를 장착해서 공격력을 추가했을때와 안했을때
            {
                Console.WriteLine("공격력 : {0}", player.attack);
            }
            else if (equipAttackstate > 0)
            {
                Console.WriteLine("공격력 : {0} (+{1})", player.attack, equipAttackstate);
            }
            if (equipDefenceState == 0) // 장비를 장착해서 방어력을 추가했을때와 안했을때
            {
                Console.WriteLine("방어력 : {0}", player.defence);
            }
            else if (equipDefenceState > 0)
            {
                Console.WriteLine("방어력 : {0} (+{1})", player.defence, equipDefenceState);
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
        } // 상태창
        // player 정보를 받아오고 player이름도 받아옴
        // equipAttackstate와 equipDefenceState는 장착하고 장착 해제시 계속해서 바뀐값이 메인에서도 적용해야 하기에 ref사용
        // secondNum은 계속해서 상세 선택할때 값이 바뀐것을 메인에도 적용하기때문에 ref 사용
        // isStartPg 처음 씬을 끄고 키는데 이 값도 메인에서 바꿔야 하기때문에 사용 // 화면이 겹치는걸 방지함
        static void InvenScene(List<Item> inven, ref int secondNum, ref int equipAttackstate, ref int equipDefenceState, ref bool isInvenPg, ref bool isStartPg)
        {
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
                            Console.WriteLine("- {0} {1}\t| 공격력 +{2} \t| {3}", i + 1, inven[i].name, inven[i].damage, inven[i].explanation);
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
                    bool isInven = int.TryParse(Console.ReadLine(), out secondNum);

                    if (isInven && secondNum == 1) // 장비 관리로 넘어갈시 초기화면 비활성화와 장비관리 활성화
                    {
                        isInvenPg = true;
                        isStartPg = false;
                    }
                    else if (!isInven || secondNum != 0) // 0이 아닐때 예외처리
                    {
                        Console.WriteLine("입력이 잘못됐습니다.");
                        Thread.Sleep(500); // 0.5초 지연
                        isStartPg = false;
                    }
                    else // 0 일때 출력
                    {
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
                    bool isInven = int.TryParse(Console.ReadLine(), out secondNum);

                    if (isInven && secondNum == 0) // 0입력시 장비관리 비활성화
                    {
                        Console.Clear();
                        isInvenPg = false;
                    }
                    else if (!isInven || (secondNum < 0) || (secondNum > inven.Count))
                    {
                        Console.WriteLine("입력이 잘못됐습니다.");
                        Thread.Sleep(500); // 0.5초 지연
                    }
                    for (int i = 0; i < inven.Count; i++) // 인벤토리갯수 만큼 반복
                    {
                        if (i + 1 == secondNum) // i는 0부터 시작해서 +1 해줌으로써 입력값과 같게 해줌
                        {
                            inven[i].isEquip = !inven[i].isEquip; // 장착 장착해제
                            if ((inven[i].itemType == 1) && (inven[i].isEquip == true)) // 무기타입이고 장착중이면 무기 데미지를 더함
                            {
                                equipAttackstate += inven[i].damage;
                            }
                            else if (inven[i].itemType == 1 && (inven[i].isEquip == false)) // 무기타입이고 장착해제면 무기 데미지를 뺌
                            {
                                equipAttackstate -= inven[i].damage;
                            }
                            else if (inven[i].itemType == 2 && (inven[i].isEquip == true)) // 방어타입이고 장착중이면 방어력을 더함
                            {
                                equipDefenceState += inven[i].defence;
                            }
                            else if (inven[i].itemType == 2 && (inven[i].isEquip == false)) // 방어타입이고 장착해제면 방어력을 뺌
                            {
                                equipDefenceState -= inven[i].defence;
                            }
                        }
                    }

                }
            }
        } // 인벤토리
        // inven이라는 리스트 받아오고 참조형이기때문에 메소드에서 수정해도 메인에서 수정적용됨
        // secondNum은 계속해서 상세 선택할때 값이 바뀐것을 메인에도 적용하기때문에 ref 사용
        // equipAttackstate와 equipDefenceState는 장착하고 장착 해제시 계속해서 바뀐값이 메인에서도 적용해야 하기에 ref사용
        // isInvenPg는 장비관리 화면을 활성화할건지 안할건지여부인데 메인에서 적용을해야 화면에 적용이되기때문에 ref사용
        // isStartPg 처음 씬을 끄고 키는데 이 값도 메인에서 바꿔야 하기때문에 사용 // 화면이 겹치는걸 방지함

        static void StoreScene(List<Item> makeItem, List<Item> inven, ref Player player, ref int secondNum, ref bool isStorePurchase, ref bool isStartPg)
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine("{0} G", player.gold);
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < makeItem.Count; i++)
            {
                if (isStorePurchase == false) // 아이템구매가 비활성화 일 경우
                {
                    if (makeItem[i].itemType == 1) // 무기일 경우 
                    {
                        if (makeItem[i].isOwn == false) // 보유하지 않았을때
                        {
                            Console.WriteLine("- {0}\t| 공격력 +{1} \t| {2} \t| {3} G", makeItem[i].name, makeItem[i].damage, makeItem[i].explanation, makeItem[i].gold);
                        }
                        else // 보유했을 경우
                        {
                            Console.WriteLine("- {0}\t| 공격력 +{1} \t| {2} \t| 구매완료", makeItem[i].name, makeItem[i].damage, makeItem[i].explanation);
                        }
                    }
                    else if (makeItem[i].itemType == 2) // 방어구일 경우
                    {
                        if (makeItem[i].isOwn == false) // 보유하지 않았을때
                        {
                            Console.WriteLine("- {0}\t| 방어력 +{1} \t| {2} \t| {3} G", makeItem[i].name, makeItem[i].defence, makeItem[i].explanation, makeItem[i].gold);
                        }
                        else // 보유했을 경우
                        {
                            Console.WriteLine("- {0}\t| 방어력 +{1} \t| {2} \t| 구매완료", makeItem[i].name, makeItem[i].defence, makeItem[i].explanation);
                        }
                    }
                }
                else
                {
                    if (makeItem[i].itemType == 1) // 무기일 경우 
                    {
                        if (makeItem[i].isOwn == false) // 보유하지 않았을때
                        {
                            Console.WriteLine("- {0} {1}\t| 공격력 +{2} \t| {3} \t| {4} G", i + 1, makeItem[i].name, makeItem[i].damage, makeItem[i].explanation, makeItem[i].gold);
                        }
                        else // 보유했을 경우
                        {
                            Console.WriteLine("- {0} {1}\t| 공격력 +{2} \t| {3} \t| 구매완료", i + 1, makeItem[i].name, makeItem[i].damage, makeItem[i].explanation);
                        }
                    }
                    else if (makeItem[i].itemType == 2) // 방어구일 경우
                    {
                        if (makeItem[i].isOwn == false) // 보유하지 않았을때
                        {
                            Console.WriteLine("- {0} {1}\t| 방어력 +{2} \t| {3} \t| {4} G", i + 1, makeItem[i].name, makeItem[i].defence, makeItem[i].explanation, makeItem[i].gold);
                        }
                        else // 보유했을 경우
                        {
                            Console.WriteLine("- {0} {1}\t| 방어력 +{2} \t| {3} \t| 구매완료", i + 1, makeItem[i].name, makeItem[i].defence, makeItem[i].explanation);
                        }
                    }
                }
            }
                if (isStorePurchase == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    bool isStore = int.TryParse(Console.ReadLine(), out secondNum);

                    if (isStore && secondNum == 1) // 아이템 구매로 넘어갈시 초기화면 비활성화와 장비관리 활성화
                    {
                        Console.Clear();
                        isStorePurchase = true;
                        isStartPg = false;
                    }
                    else if (!isStore || secondNum != 0) // 0이 아닐때 예외처리
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
                else // 아이템 구매가 활성화 됐을때의 조건문
                {
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    bool isStore = int.TryParse(Console.ReadLine(), out secondNum);

                    if (isStore && secondNum == 0) // 0입력시 아이템구매 비활성화
                    {
                        Console.Clear();
                        isStorePurchase = false;
                    }
                    else if (isStore == false || (secondNum < 0) || (secondNum > makeItem.Count))
                    {
                        Console.WriteLine("입력이 잘못됐습니다!");
                        Thread.Sleep(500); // 0.5초 지연
                    }

                    for (int j = 0; j < makeItem.Count; j++) // 아이템갯수 만큼 반복
                    {
                        if (j + 1 == secondNum) // j는 0부터 시작해서 +1 해줌으로써 입력값과 같게 해줌
                        {
                             if ((player.gold >= makeItem[j].gold) && (makeItem[j].isOwn == false))
                             {
                                 player.gold -= makeItem[j].gold;
                                 makeItem[j].isOwn = true;
                                 inven.Add(makeItem[j]);
                                 Console.WriteLine("구매를 완료했습니다.");
                                 Thread.Sleep(500); // 0.5초 지연
                             }
                             else if (player.gold < makeItem[j].gold)
                             {
                                 Console.WriteLine("Gold가 부족합니다...");
                                 Thread.Sleep(500); // 0.5초 지연
                             }
                             else if (makeItem[j].isOwn == true)
                             {
                                Console.WriteLine("이미 구매한 아이템입니다.");
                                Thread.Sleep(500); // 0.5초 지연
                             }
                        }

                    }

            }

            
        }
        // makeItem이라는 아이템들이 모여있는 리스트 가져옴
        // inven이라는 인벤토리 아이템리스트 불러오고 구매시 makeItem의 아이템을 inven에 Add
        // Player의 gold값을 변경해야 하기 때문에 ref사용
        // secondNum은 계속해서 상세 선택할때 값이 바뀐것을 메인에도 적용하기때문에 ref 사용
        // isStorePurchase는 장비구매 화면을 활성화할건지 안할건지여부인데 메인에서 적용을해야 화면에 적용이되기때문에 ref사용
        // isStartPg 처음 씬을 끄고 키는데 이 값도 메인에서 바꿔야 하기때문에 사용 // 화면이 겹치는걸 방지함
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

            string playerName = CreateName(); // 이름 생성
            SettingPlayerState(out Player player); // 플레이어 정보 설정


            int firstNum = 0; // 초기에 1,2,3 화면전환을 위한 변수
            int secondNum = 1; // 화면 전환 후 선택을 위한 변수

            int equipAttackstate = 0; // 장착 공격력 변수
            int equipDefencestate = 0; // 장착 방어력 변수
            
            bool isTrue = false;
            bool isStartPg = true; //시작 화면 활성화 여부
            bool isInvenPg = false; // 인벤토리 관리 활성화 여부
            bool isStorePurchase = false; // 상점구매 활성화 여부

            
            while (true)
            {
                if (isStartPg)
                {
                    isTrue = FirstScene(isTrue, out firstNum); // 초기화면
                }
                if (isTrue && firstNum == 1)
                {
                    // 상태
                    StateScene(player, playerName, ref equipAttackstate, ref equipDefencestate, ref secondNum, ref isStartPg);
                }
                else if (isTrue && firstNum == 2)
                {
                    //인벤토리
                    InvenScene(inven, ref secondNum, ref equipAttackstate, ref equipDefencestate, ref isInvenPg, ref isStartPg);
                }
                else if (isTrue && firstNum == 3)
                {
                    // 상점
                    StoreScene(makeItem, inven, ref player, ref secondNum, ref isStorePurchase, ref isStartPg);
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
