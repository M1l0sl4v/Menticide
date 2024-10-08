using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherManager : MonoBehaviour
{
   public static WeatherManager instance;
   
   public WeatherType currentWeather;

   public enum WeatherType
   {
      Sunny,
      Rainy,
      autumnLeaves,
      springLeaves,
      Snowing,
      windyautumn,
      windySpring,
      Foggy,
   }

   private void Start()
   {
      instance = this;
   }


   //okay so this is a little fucky but I wanna be able to test with an enum, not sure if this will work
   //but bascially to add a new weather you add to the enum, then whatever seasosns you want it to effect, then 
   //the switch statement
   public void ChangeWeather()
   {
      switch (seasons.instance.currentSeason)
      {
         case 1: //sumner
            currentWeather = GetRandomWeather(WeatherType.Sunny, WeatherType.Rainy, WeatherType.Foggy);
            break;
         case 2://autumn
            currentWeather = GetRandomWeather(WeatherType.autumnLeaves, WeatherType.Rainy, WeatherType.Foggy, WeatherType.windyautumn);
            break;
         case 3: //winter
            currentWeather = GetRandomWeather(WeatherType.Snowing, WeatherType.Rainy, WeatherType.Foggy);
            break;
         case 4: // spring
            currentWeather = GetRandomWeather(WeatherType.Sunny, WeatherType.Rainy, WeatherType.Foggy, WeatherType.springLeaves, WeatherType.windySpring);
            break;
      }
      Debug.Log(currentWeather);
      ApplyWeatherEffect(currentWeather);
   }
   
   private WeatherType GetRandomWeather(params WeatherType[] possibleWeathers)
   {
      int index = Random.Range(0, possibleWeathers.Length);
      return possibleWeathers[index];
   }

   private void ApplyWeatherEffect(WeatherType weather) 
   {
      switch (weather)
      {
         case WeatherType.Sunny:
            sunny();
            break;
         case WeatherType.Rainy:
            rainy();
            break;
         case WeatherType.windyautumn:
            windyAutumn();
            break;
         case WeatherType.autumnLeaves:
            autumnLeaves();
            break;
         case WeatherType.windySpring:
            windySpring();
            break;
         case WeatherType.springLeaves:
            springLeaves();
            break;
         case WeatherType.Foggy:
            foggy();
            break;
         case WeatherType.Snowing:
            snowing();
            break;
      }
   }
   // I KNOW WE DONT NEED THE METHODS, this is mostly just so we dont have a switch statement the size of the bible
   public void sunny()
   {
      
   }
   public void rainy()
   {
      
   } 
   public void windyAutumn()
   {
      
   } 
   public void autumnLeaves()
   {
      
   } 
   public void windySpring()
   {
      
   } 
   public void springLeaves()
   {
      
   } 
   public void foggy()
   {
      
   }
   public void snowing()
   {
      
   } 
}
