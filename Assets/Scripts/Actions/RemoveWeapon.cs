namespace GamePlay.Actions
{
    public class RemoveWeapon : IAction
    {
        private bool isFinish = false;
        public override bool Finished => isFinish;

        private Enemy enemy;

        public RemoveWeapon(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Act()
        {
            enemy.RemoveWeapon();
            isFinish = true;
        }

        public override IAction Duplicate()
        {
            return new RemoveWeapon(enemy);
        }
    }
}
