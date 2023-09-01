using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    private Player_Controller _player;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private AudioSource footAudio;
    [SerializeField]
    private AudioSource AreafootAudio;

    [SerializeField]
    private AudioClip footsteps;
    [SerializeField]
    private AudioClip oceanfootsteps;
    [SerializeField]
    private AudioClip sandfootsteps;
    [SerializeField]
    private AudioClip swimSe;

    private Area_Controller area_Controller;

    // Start is called before the first frame update
    void Start()
    {
        _collider.enabled = false;
        _player = transform.root.gameObject.GetComponent<Player_Controller>();

        area_Controller = GameObject.Find("Area").GetComponent<Area_Controller>();
    }

    private void Update()
    {
        //Debug.Log(area_Controller.AreaNumber);
    }

    public void isMove()
    {
        _player.isMove = true;
    }

    public void isGet()
    {
        _collider.enabled = true;
    }
    public void isNotGet()
    {
        _collider.enabled = false;
    }
    public void FootSteps()
    {
        if (_player.foots)
        {
            footAudio.PlayOneShot(footsteps);
            AreafootAudio.PlayOneShot(area_Controller.area[area_Controller.AreaNumber].footSteps);
        }
        if (_player.oceanFoots)
        {
            AreafootAudio.PlayOneShot(oceanfootsteps);
        }
        if(_player.sandFoots)
        {
            AreafootAudio.PlayOneShot(sandfootsteps);
        }
    }
    public void SwimSe()
    {
        if (!(_player.move.x == 0 && _player.move.y == 0 && _player.move.z == 0))
        {
            AreafootAudio.PlayOneShot(swimSe);
        }
    }
}
