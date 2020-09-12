using UnityEngine;

namespace GamePlay.Actions
{
    public class GenerateObject : IAction
    {
        private bool isFinish = false;
        public override bool Finished => isFinish;

        private GameObject ob;
        private float maxLoad;
        private float curLoad;
        private int count;
        public GenerateObject(GameObject ob, float maxLoad, float curLoad, int count)
        {
            this.ob = ob;
            this.maxLoad = maxLoad;
            this.curLoad = curLoad;
            this.count = count;
        }

        public override void Act()
        {
            curLoad += Time.deltaTime;
            isFinish = curLoad >= maxLoad;
            if (curLoad >= maxLoad)
            {
                curLoad = 0f;
                for (int i = 0; i < count; i++)
                {
                    GameObject go = GameObject.Instantiate(ob);
                    go.SetActive(true);
                    float x = Random.Range(-5.5f, 5.5f);
                    float y = Random.Range(-5.0f, 5.0f);
                    go.transform.position = new Vector3(x, y, 0.0f);
                    Enemy enemy = go.GetComponent<Enemy>();
                    if (enemy != null)
                        enemy.StartAction();
                    Suicide suicide = go.GetComponent<Suicide>();
                    if (suicide != null)
                        suicide.StartCountTime();
                }
            }
        }

        public override IAction Duplicate()
        {
            return new GenerateObject(ob, maxLoad, curLoad, count);
        }
    }
}