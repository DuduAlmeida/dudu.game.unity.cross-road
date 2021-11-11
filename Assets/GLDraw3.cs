using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GLDraw3 : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    bool sg = false;
    public float by = 0;
    float bx = 0;
    float velo = 0.05f;
    bool shoot = false;
    public float mGL = -2;
    public float mGR = 2;
    bool invert;
    public int life = 3;
    public int score = 0;
    public Text tL;
    public Text tS;

    #region Unity Methods
    private void Start()
    {
        tL.text = life.ToString();
        tS.text = score.ToString();
        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    private void Update()
    {
        tL.text = life.ToString();
        tS.text = score.ToString();

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
       
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            bx -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            bx += velo;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            shoot = true;
            by += velo;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            shoot = false;
            by = 0;
        }
        Move();
        Collision();
    }
    private void OnPostRender()
    {
        if (sg)
        {
            BarRight();
            BarGol();
            BarLeft();
            BarBottom();
            Ball();            
        }
    }
    #endregion

    #region My Methods
    public void SatartGame()
    {
        sg = true;
    }
    void Ball()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.red);

        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx, by+1, 0);
        GL.Vertex3(bx+1, by+1, 0);
        GL.Vertex3(bx+1, by, 0);

        GL.End();
        GL.PopMatrix();
    }
    void BarGol()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);

        GL.Vertex3(sb.x * (-1), sb.y , 0);
        GL.Vertex3(sb.x * (-1), sb.y - 1, 0);
        GL.Vertex3(mGL, sb.y - 1, 0);
        GL.Vertex3(mGL, sb.y, 0);

        GL.Vertex3(mGR, sb.y, 0);
        GL.Vertex3(mGR, sb.y - 1, 0);
        GL.Vertex3(sb.x, sb.y - 1, 0);
        GL.Vertex3(sb.x, sb.y, 0);

        GL.End();
        GL.PopMatrix();
    }
    void BarRight()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        GL.Vertex3(sb.x, sb.y * (-1), 0);
        GL.Vertex3(sb.x, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y, 0);
        GL.Vertex3(sb.x - 1, sb.y * (-1), 0);
        GL.End();
        GL.PopMatrix();
    }
    void BarLeft()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        GL.Vertex3(sb.x * (-1),sb.y*(-1),0);
        GL.Vertex3(sb.x * (-1), sb.y, 0);
        GL.Vertex3(sb.x * (-1)+1, sb.y, 0);
        GL.Vertex3(sb.x * (-1)+1, sb.y * (-1), 0);
        GL.End();
        GL.PopMatrix();
    }
    void BarBottom()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.Color(Color.yellow);
        GL.Vertex3(sb.x *(-1), sb.y*(-1),0);
        GL.Vertex3(sb.x * (-1), sb.y * (-1)+1, 0);
        GL.Vertex3(sb.x, sb.y * (-1)+1, 0);
        GL.Vertex3(sb.x, sb.y*(-1),0);
        GL.End();
        GL.PopMatrix();
    }
    void Move()
    {
        if(shoot)
            by += velo;

        if (mGL > sb.x * (-1) && invert)
        {
            mGR -= velo;
            mGL -= velo;
        }
        else 
        {
            invert = false;
        }
        if (mGR < sb.x && !invert)
        {
            mGR += velo;
            mGL += velo;
        }
        else
        {
            invert = true;
        }

    }
    void Collision()
    {
        //Trave collision
        if((by+1) >= (sb.y-1) && (bx <= mGL || bx+1 >= mGR))
        {
            life--;
            by = 0;
            shoot = false;
        }
        //Gol collision
        else if (by >= sb.y)
        {
            velo += 0.05f;
            score++;
            by = 0;
            shoot = false;
        }
    }
    #endregion
}
