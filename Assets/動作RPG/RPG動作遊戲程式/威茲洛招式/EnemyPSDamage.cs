using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{
    public class EnemyPSDamage : MonoBehaviour
    {
        public int damageAmount;

        public ParticleSystem part;
        public List<ParticleCollisionEvent> collisionEvents;

        public ParticleSystem shock;

        private void Start()
        {
            part = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
        }
        //发生粒子碰撞的回调函数
        private void OnParticleCollision(GameObject player)
        {
            print(player.name);

            if (player.CompareTag("Player"))
            {
                int damage = damageAmount;

                print(player.name + "受到傷害");

                Instantiate(shock, player.transform.position, shock.transform.rotation);

                damage -= player.transform.GetComponent<RPGCharacter>().def;
                StartCoroutine(player.transform.GetComponent<RPGCharacter>().TakeDamage(damage, 0.3f));
            }
        }

        //粒子触发的回调函数
        private void OnParticleTrigger()
        {
            //只要勾选了粒子系统的trigger，程序运行后会一直打印


            //官方示例，拿来说明
            ParticleSystem ps = transform.GetComponent<ParticleSystem>();

            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
            List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
            //particleSystemTriggerEventType为枚举类型，Enter,Exit,Inside,Outside,对应粒子系统属性面板上的四个选项
            int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
            //进入触发器，粒子变为红色
            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = Color.blue;
                enter[i] = p;
            }
            
            /*
            //退出触发器 粒子变为蓝绿色
            for (int i = 0; i < numExit; i++)
            {
                ParticleSystem.Particle p = exit[i];
                p.startColor = Color.cyan;
                exit[i] = p;
            }
            */

            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        }
    }
}
