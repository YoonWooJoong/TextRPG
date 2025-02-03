namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
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

            int selecNum;
            bool isTrue = int.TryParse(Console.ReadLine(),out selecNum);
            switch(selecNum)
            {
                case 1:
                    //상태보기
                    break;
                case 2:
                    // 인벤토리
                    break;
                case 3:
                    // 상점
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

        }
    }
}
