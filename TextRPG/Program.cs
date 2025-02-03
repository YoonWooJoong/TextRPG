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
        static void Main(string[] args)
        {
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
