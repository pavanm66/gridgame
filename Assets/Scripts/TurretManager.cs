//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TurretManager : MonoBehaviour
//{
//    Turret[] turrets;

//    void Awake()
//    {
//        turrets = GetComponentsInChildren<Turret>();
//    }

//    public void Replay()
//    {
//        if (turrets.Length>0)
//        {
//            foreach (var turret in turrets)
//            {
//                turret.Replay();
//            }
//        }
//    }
//    internal void GameOver()
//    {
//        if (turrets.Length > 0)
//        {
//            foreach (var turret in turrets)
//            {
//                turret.GameOver();
//            }
//        }
//    }
//    public void CheckPlayerStatus(bool _isPlayerAlive)
//    {
//        if (turrets.Length > 0)
//        {
//            foreach (var turret in turrets)
//            {
//                turret.CheckPlayerStatus(_isPlayerAlive);
//            }
//        }
//    }
//}
