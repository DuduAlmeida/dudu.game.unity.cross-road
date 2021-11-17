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

    if (Input.GetKey(KeyCode.LeftArrow))
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
      // CarMustang();
      // Motorcycle();
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
    GL.Vertex3(bx, by + 1, 0);
    GL.Vertex3(bx + 1, by + 1, 0);
    GL.Vertex3(bx + 1, by, 0);

    GL.End();
    GL.PopMatrix();
  }
  void BarGol()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);

    GL.Vertex3(sb.x * (-1), sb.y, 0);
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
    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y, 0);
    GL.Vertex3(sb.x * (-1) + 1, sb.y, 0);
    GL.Vertex3(sb.x * (-1) + 1, sb.y * (-1), 0);
    GL.End();
    GL.PopMatrix();
  }
  void BarBottom()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);
    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y * (-1) + 1, 0);
    GL.Vertex3(sb.x, sb.y * (-1) + 1, 0);
    GL.Vertex3(sb.x, sb.y * (-1), 0);
    GL.End();
    GL.PopMatrix();
  }
  void CarMustang()
  {
    Color carColor = Color.white;
    carColor.r = 255;
    carColor.g = 165;
    carColor.b = 0;
    const int carWidth = 6;
    const int carHeight = 13;
    const float wheelHeight = 2.0f;
    const float wheelWidth = 0.5f;

    GL.PushMatrix();
    mat.SetPass(0);

    // #region Preencher Carcaça Carro
    GL.Begin(GL.QUADS);
    GL.Color(carColor);
    GL.Vertex(new Vector3(bx, by, 0));
    GL.Vertex(new Vector3(bx, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + carHeight, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + carHeight, by, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(carColor);
    GL.Vertex(new Vector3(bx + carHeight, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 1.05f), by + (carWidth / 2), 0));
    GL.Vertex(new Vector3(bx + carHeight, by, 0));
    GL.Vertex(new Vector3(bx + carHeight, by, 0));
    GL.End();
    // #endregion Preencher Carcaça Carro
    // #region Preencher Pneus
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by + carWidth, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by + carWidth, 0));
    GL.End();
    // #endregion Preencher Pneus

    GL.Begin(GL.LINES);
    GL.Color(Color.black);

    // #region Carro                
    // #region Carro#Carcaça            
    GL.Vertex(new Vector3(bx, by, 0));
    GL.Vertex(new Vector3(bx, by + carWidth, 0));
    GL.Vertex(new Vector3(bx, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + carHeight, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + carHeight, by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 1.05f), by + (carWidth / 2), 0));
    GL.Vertex(new Vector3(bx + (carHeight * 1.05f), by + (carWidth / 2), 0));
    GL.Vertex(new Vector3(bx + carHeight, by, 0));
    GL.Vertex(new Vector3(bx + carHeight, by, 0));
    GL.Vertex(new Vector3(bx, by, 0));
    // #endregion Carro#Carcaça
    // #region Capô
    GL.Vertex(new Vector3(bx + carHeight, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 3, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 3, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4, by + carWidth - 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4, by + carWidth - 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4, by + 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4, by + 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 3, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 3, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight, by + 0.5f, 0));
    // #region Centro Capô
    GL.Vertex(new Vector3(bx + carHeight - 3, by + carWidth - 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 2, by + carWidth - 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 2, by + carWidth - 2, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 1.05f) - 2, by + (carWidth / 2), 0));
    GL.Vertex(new Vector3(bx + (carHeight * 1.05f) - 2, by + (carWidth / 2), 0));
    GL.Vertex(new Vector3(bx + carHeight - 2, by + 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 2, by + 2, 0));
    GL.Vertex(new Vector3(bx + carHeight - 3, by + 2, 0));
    // #endregion Centro Capô
    // #endregion Capô
    // #region Porta Malas
    GL.Vertex(new Vector3(bx, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + 2f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + 2f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + 2f, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + 2f, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx, by + 0.5f, 0));
    // #endregion Porta Malas
    // #region Janela dianteira
    GL.Vertex(new Vector3(bx + carHeight - 4.5f, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5.5f, by + 1.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5.5f, by + 1.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5.5f, by + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5.5f, by + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4.5f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4.5f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 4.5f, by + 0.5f, 0));
    // #endregion Janela dianteira
    // #region Janela traseira
    GL.Vertex(new Vector3(bx + 2.5f, by + 1, 0));
    GL.Vertex(new Vector3(bx + 3, by + 1.5f, 0));
    GL.Vertex(new Vector3(bx + 3, by + 1.5f, 0));
    GL.Vertex(new Vector3(bx + 3, by + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(bx + 3, by + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(bx + 2.5f, by + carWidth - 1, 0));
    GL.Vertex(new Vector3(bx + 2.5f, by + carWidth - 1, 0));
    GL.Vertex(new Vector3(bx + 2.5f, by + 1, 0));
    // #endregion Janela traseira
    // #region Janela lateral esquerda
    GL.Vertex(new Vector3(bx + 3, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5f, by + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 6f, by + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 6f, by + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 7f, by + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 7f, by + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(bx + 3, by + carWidth - 0.5f, 0));
    // #endregion Janela lateral esquerda
    // #region Janela lateral direita
    GL.Vertex(new Vector3(bx + 3, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5f, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 5f, by + 0.5f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 6f, by + 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 6f, by + 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 7f, by + 1.3f, 0));
    GL.Vertex(new Vector3(bx + carHeight - 7f, by + 1.3f, 0));
    GL.Vertex(new Vector3(bx + 3, by + 0.5f, 0));
    // #endregion Janela lateral direita
    // #region Pneu Dianteiro Direita
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by + carWidth, 0));
    // #endregion Pneu Dianteiro Direita
    // #region Pneu Dianteiro Esquerda
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, by, 0));
    // #endregion Pneu Dianteiro Esquerda
    // #region Pneu Traseiro Direita
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by + carWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by + carWidth, 0));
    // #endregion Pneu Traseiro Direita
    // #region Pneu Traseiro Esquerda
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f), by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by - wheelWidth, 0));
    GL.Vertex(new Vector3(bx + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, by, 0));
    // #endregion Pneu Traseiro Esquerda
    // #endregion Carro

    GL.End();
    GL.PopMatrix();
  }
  void Motorcycle()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.LINES);

    // #region contorno motoca
    GL.Vertex(new Vector3(bx, by, 0));
    GL.Vertex(new Vector3(bx, by + 1, 0));
    GL.Vertex(new Vector3(bx, by + 1, 0));
    GL.Vertex(new Vector3(bx + 1, by + 1, 0));
    GL.Vertex(new Vector3(bx + 1, by + 1, 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + 4, 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + 4, 0));
    GL.Vertex(new Vector3(bx + 6, by + (float)3.8, 0));
    GL.Vertex(new Vector3(bx + 6, by + (float)3.8, 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + (float)9.5, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + (float)9.5, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + (float)14.5, by + 2, 0));
    GL.Vertex(new Vector3(bx + (float)14.5, by + 2, 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + 17, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + 17, by + (float)1.5, 0));
    GL.Vertex(new Vector3(bx + 17, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + 17, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + (float)14.5, by + (-1), 0));
    GL.Vertex(new Vector3(bx + (float)14.5, by + (-1), 0));
    GL.Vertex(new Vector3(bx + 16, by + (-1), 0));
    GL.Vertex(new Vector3(bx + 16, by + (-1), 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)(-1.8), 0));
    GL.Vertex(new Vector3(bx + 16, by + (float)(-1.8), 0));
    GL.Vertex(new Vector3(bx + (float)12.5, by + (float)(-1.8), 0));
    GL.Vertex(new Vector3(bx + (float)12.5, by + (float)(-1.8), 0));
    GL.Vertex(new Vector3(bx + (float)9.5, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + (float)9.5, by + (float)(-0.5), 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)(-1.5), 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)(-1.5), 0));
    GL.Vertex(new Vector3(bx + 6, by + (float)(-2.8), 0));
    GL.Vertex(new Vector3(bx + 6, by + (float)(-2.8), 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (-3), 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (-3), 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)(-1.5), 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)(-1.5), 0));
    GL.Vertex(new Vector3(bx + 1, by, 0));
    GL.Vertex(new Vector3(bx + 1, by, 0));
    GL.Vertex(new Vector3(bx, by, 0));
    // #endregion contorno motoca
    // #region linhas internas motoca
    GL.Vertex(new Vector3(bx + 5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)(-1.5), 0));

    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + (float)5.5, by + (float)(-1.5), 0));

    GL.Vertex(new Vector3(bx + (float)14.5, by + (-1), 0));
    GL.Vertex(new Vector3(bx + (float)9.5, by + (float)(-0.5), 0));

    GL.Vertex(new Vector3(bx + 5, by + (float)2.5, 0));
    GL.Vertex(new Vector3(bx + 3, by + (float)0.5, 0));
    GL.Vertex(new Vector3(bx + 3, by + (float)0.5, 0));
    GL.Vertex(new Vector3(bx + 5, by + (float)(-1.5), 0));
    // #endregion linhas internas motoca

    GL.End();
    GL.PopMatrix();
  }
  void Move()
  {
    if (shoot)
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
    if ((by + 1) >= (sb.y - 1) && (bx <= mGL || bx + 1 >= mGR))
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
