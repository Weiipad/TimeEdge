namespace GamePlay.Actions
{
    public class EquipWeapon : IAction
    {
        private bool isFinish = false;
        public override bool Finished => isFinish;

        private Weapon weapon;
        private Enemy enemy;

        public EquipWeapon(Enemy enemy, Weapon weapon)
        {
            this.enemy = enemy;
            this.weapon = weapon;
        }

        public override void Act()
        {
            enemy.EquipWeapon(weapon);
            isFinish = true;
        }

        public override IAction Duplicate()
        {
            return new EquipWeapon(enemy, weapon);
        }
    }
}
