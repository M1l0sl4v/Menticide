using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;


public class WeatherManager : MonoBehaviour
{
   public static WeatherManager instance;

   public float fallSpeed;
   public float springSpeed;
   
   public float defaultSpeed = 2.9f;
   
   //gameobjects
   public GameObject rainOverlay;
   public GameObject snowOverlay;
   public GameObject windOverlay;
   public GameObject springLeaf;
   public GameObject fallLeaf;
   public GameObject summerLeaf;

   public Material fogMaterial;
   public Light2D playerLight;

   private float fogOpacity;
   private Animator snowSpeed;
   private Animator windSpeed;
   private ParticleSystem springParticles;
   private ParticleSystem fallParticles;
   
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
      ChangeWeather();
      snowSpeed = snowOverlay.GetComponent<Animator>();
      windSpeed = windOverlay.GetComponent<Animator>();
      springParticles = springLeaf.GetComponent<ParticleSystem>();
      fallParticles = fallLeaf.GetComponent<ParticleSystem>();
   }


   //okay so this is a little fucky but I wanna be able to test with an enum, not sure if this will work
   //but bascially to add a new weather you add to the enum, then whatever seasosns you want it to effect, then 
   //the switch statement
   public void ChangeWeather()
   {
      switch (seasons.instance.currentSeason)
      {
         case 1: //sumner
            currentWeather = GetRandomWeather(WeatherType.Sunny, WeatherType.Rainy);
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
      
      // all of this sets the lighitng and fog back to "normal" before the seasons change, just for consistancy.
      Vector2 fogSpeed = new Vector2(0.5f, 0.5f); 
      fogMaterial.SetVector("_fogSpeed", fogSpeed); 
      playerLight.color = new Color(1, 1, 1, 1);
      fogOpacity = 0.03f;
      fogMaterial.SetFloat("_opacity", fogOpacity);
      
      
      ///i cant get this to work without breaking
      /*
      var fallDefault = fallParticles.main;
      fallDefault.startSpeed = defaultSpeed;
      
      var springDefault = springParticles.main;
      springDefault.startSpeed = defaultSpeed;
      */
      
      
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
      setWeatherObjects(false,false,false,false,false,false,true);
      playerLight.color = new Color(1f, 0.9f, 0.6f,1);
   }
   public void rainy()
   {
      setWeatherObjects(false,true,false,false,false,false,false);
      playerLight.color = new Color(.6f, 0.9f, 1f,1);

   } 
   public void windyAutumn()
   {
      setWeatherObjects(false, false,false,false,true,true, false);
      float windspeed = windSpeed.speed;
      Vector2 fogSpeed = new Vector2(windspeed, 0f); 
      fogMaterial.SetVector("_fogSpeed", fogSpeed); 
      playerLight.color = new Color(1, 1, 1, 1);

      var fallModule = fallParticles.main;
      fallModule.startSpeed = fallSpeed;
   } 
   public void autumnLeaves()
   {
      setWeatherObjects(false, false,false,false,false,true, false);
      playerLight.color = new Color(1,1,1,1);
   } 
   public void windySpring()
   {
      setWeatherObjects(true, false,false,false,true,false, false);
      float windspeed = windSpeed.speed;
      Vector2 fogSpeed = new Vector2(windspeed, 0f); 
      fogMaterial.SetVector("_fogSpeed", fogSpeed); 
      playerLight.color = new Color(1f, 0.9f, 0.7f);
      
      var springModule = springParticles.main;
      springModule.startSpeed = springSpeed; 
   } 
   public void springLeaves()
   {
      setWeatherObjects(true, false,false,false,false,false, false);
      playerLight.color = new Color(1f, 0.9f, 0.7f);
   } 
   public void foggy()
   {
      setWeatherObjects(false, false,true,false,false,true, false);
      playerLight.color = new Color(0.73f, 0.73f, 0.7f);
      Vector2 fogSpeed = new Vector2(0.2f, 0.2f); 
      fogMaterial.SetVector("_fogSpeed", fogSpeed); 
      fogOpacity = 0.07f;
      fogMaterial.SetFloat("_opacity", fogOpacity);
   }
   public void snowing()
   {
      setWeatherObjects(false, false,false,true,false,false, false);
      playerLight.color = Color.white;
      fogOpacity = 0.05f;
      fogMaterial.SetFloat("_opacity", fogOpacity);
   } 
   public void setWeatherObjects(bool spring, bool rain, bool foggy, bool snow, bool wind, bool fall, bool summerLeaves)
   {
      rainOverlay.SetActive(rain);
      snowOverlay.SetActive(snow);
      windOverlay.SetActive(wind);
      fallLeaf.SetActive(fall);
      springLeaf.SetActive(spring);
      summerLeaf.SetActive(summerLeaves);
      fogOpacity = 0.05f;
      fogMaterial.SetFloat("_opacity", fogOpacity);

   }

   private void OnValidate()
   {
      ApplyWeatherEffect(currentWeather);
   }
}
