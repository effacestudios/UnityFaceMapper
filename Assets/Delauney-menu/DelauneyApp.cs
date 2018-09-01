using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Delauney;
using System.IO;
using System.Linq;
using System.Text;

/**
 * DelauneyApp: This class maps one texture into another by means of delauney triangulation.
 * 				
 * 				We first define set of points in the 'standard' texture, which is triangulated
 * 				using a Delayney incremental alghorithm.
 * 
 * 				The, an input texture, with a similar set of points, should be supplyed. The 
 * 				set of points in, then, triangulated.
 * 
 * 				After this, the input image is mapped onto a blank texture, via barycentric coordinates
 * 				of the user points triangulation onto the 'standard' triangulation.
 * 
 * 				Usage: DelauneyApp.makeFaceText( inputTexture, inputPoints);
 * 
 */
public class DelauneyApp : MonoBehaviour {
    public bool LearningMode = false;

    public Renderer modelRenderer;
    public Renderer inputImageRenderer;
    public List<FaceElement> faceElements;

    private Texture2D modelTexture;
    private Texture2D inputFaceImage;

    private List<Point> modelFacePoints = new List<Point>
    {/*
        new Point(0,511.3192f,570.662f),
        new Point(1,558.9448f,576.8235f),
        new Point(2,615.7903f,597.7908f),
        new Point(3,656.7317f,618.5313f),
        new Point(4,716.549f,651.4627f),
        new Point(5,676.4764f,761.0786f),
        new Point(6,668.3626f,812.9669f),
        new Point(7,638.394f,833.5438f),
        new Point(8,560.9422f,860.6359f),
        new Point(9,465.7699f,861.1134f),
        new Point(10,394.5024f,831.6175f),
        new Point(11,347.0872f,814.8932f),
        new Point(12,338.9621f,760.8358f),
        new Point(13,308.824f,662.5032f),
        new Point(14,369.3334f,614.1931f),
        new Point(15,406.8367f,590.8377f),
        new Point(16,471.1602f,577.8103f),
        new Point(17,511.3192f,576.9552f),
        new Point(18,559.6595f,586.985f),
        new Point(19,588.2553f,613.2537f),
        new Point(20,612.766f,640.4776f),
        new Point(21,628.4255f,677.7313f),
        new Point(22,629.1064f,729.7911f),
        new Point(23,610.7234f,769.4329f),
        new Point(24,582.8085f,782.806f),
        new Point(25,537.1915f,790.4478f),
        new Point(26,488.8511f,790.9254f),
        new Point(27,439.1489f,783.7612f),
        new Point(28,409.1914f,768.4777f),
        new Point(29,392.8511f,730.2687f),
        new Point(30,388.0851f,678.6866f),
        new Point(31,402.383f,643.3433f),
        new Point(32,429.617f,611.3433f),
        new Point(33,464.3404f,587.9403f),
        new Point(34,453.4016f,738.9291f),
        new Point(35,424.125f,738.9291f),
        new Point(36,482.6782f,738.9291f),
        new Point(37,453.4016f,753.2574f),
        new Point(38,453.4016f,724.6007f),
        new Point(39,567.1828f,740.6047f),
        new Point(40,537.9062f,740.6047f),
        new Point(41,596.4594f,740.6047f),
        new Point(42,567.1828f,754.933f),
        new Point(43,567.1828f,726.2763f),
        new Point(44,289.8731f,714.3043f),
        new Point(45,266.8123f,712.889f),
        new Point(46,312.9339f,715.7196f),
        new Point(47,283.9827f,761.5341f),
        new Point(48,295.7635f,667.0745f),
        new Point(49,726.5471f,715.9799f),
        new Point(50,703.4864f,717.3953f),
        new Point(51,749.608f,714.5646f),
        new Point(52,732.5331f,763.976f),
        new Point(53,720.5612f,667.9838f),
        new Point(54,510.3035f,704.3775f),
        new Point(55,487.1546f,704.3775f),
        new Point(56,533.4525f,704.3775f),
        new Point(57,510.3035f,742.1881f),
        new Point(58,510.3035f,666.5669f),
        new Point(59,511.877f,635.0531f),
        new Point(60,470.2608f,635.0531f),
        new Point(61,553.4933f,635.0531f),
        new Point(62,511.877f,645.6389f),
        new Point(63,511.877f,624.4674f),*/new Point(0,512.429f,569.2031f),
new Point(1,561.9892f,575.4302f),
new Point(2,612.4074f,600.8875f),
new Point(3,643.2323f,620.2688f),
new Point(4,712.4766f,658.5491f),
new Point(5,671.0424f,772.7189f),
new Point(6,667.1818f,809.9827f),
new Point(7,627.0828f,835.9694f),
new Point(8,570.1758f,855.8851f),
new Point(9,457.4794f,856.391f),
new Point(10,410.2876f,836.9813f),
new Point(11,356.1658f,823.1558f),
new Point(12,343.6715f,776.7711f),
new Point(13,310.007f,661.9252f),
new Point(14,379.4687f,613.8478f),
new Point(15,411.369f,590.5892f),
new Point(16,482.9212f,573.9172f),
new Point(17,512.429f,581.9884f),
new Point(18,558.3279f,584.3384f),
new Point(19,588.5004f,618.0748f),
new Point(20,610.2626f,648.0948f),
new Point(21,625.1313f,687.5573f),
new Point(22,625.7778f,742.7036f),
new Point(23,608.3232f,784.6957f),
new Point(24,581.8182f,798.8617f),
new Point(25,538.5051f,806.9565f),
new Point(26,492.606f,807.4625f),
new Point(27,445.4142f,799.8735f),
new Point(28,416.9697f,783.6838f),
new Point(29,401.4546f,743.2095f),
new Point(30,396.9293f,688.5692f),
new Point(31,410.505f,651.1304f),
new Point(32,433.3427f,616.0511f),
new Point(33,464.802f,584.1681f),
new Point(34,459.1953f,740.174f),
new Point(35,431.3973f,740.174f),
new Point(36,486.9933f,740.174f),
new Point(37,459.1953f,750.6235f),
new Point(38,459.1953f,729.7244f),
new Point(39,571.7043f,742.3679f),
new Point(40,543.9064f,742.3679f),
new Point(41,599.5023f,742.3679f),
new Point(42,571.7043f,752.8174f),
new Point(43,571.7043f,731.9183f),
new Point(44,290.6372f,715.8846f),
new Point(45,268.8713f,713.4906f),
new Point(46,312.4031f,718.2787f),
new Point(47,282.1565f,763.11f),
new Point(48,299.118f,668.6594f),
new Point(49,728.8134f,720.4427f),
new Point(50,707.0475f,722.8367f),
new Point(51,750.5793f,718.0487f),
new Point(52,737.5349f,769.0089f),
new Point(53,720.0919f,671.8765f),
new Point(54,513.5104f,700.8959f),
new Point(55,490.0202f,700.8959f),
new Point(56,537.0007f,700.8959f),
new Point(57,513.5104f,741.0533f),
new Point(58,513.5104f,660.7383f),
new Point(59,513.9337f,635.481f),
new Point(60,477.3445f,635.481f),
new Point(61,550.5229f,635.481f),
new Point(62,513.9337f,646.6943f),
new Point(63,513.9337f,624.2676f),
new Point(64,514.5859f,558.588f),
new Point(65,758.4842f,683.3301f),
new Point(66,965.51f,600.7881f),
new Point(67,965.5101f,510.4869f),
new Point(68,738.4258f,521.7971f),
new Point(69,515.4498f,467.9511f),
new Point(70,282.9763f,521.6268f),
new Point(71,55.68054f,512.1749f),
new Point(72,55.68051f,599.4406f),
new Point(73,279.5386f,679.9542f),
    };
    private List<Point> inputFacePoints = new List<Point>();
    private Triangulation modelFaceTriangulation;

	void Start () {
        if (modelRenderer == null) modelRenderer = GetComponent<Renderer>();

		modelTexture = modelRenderer.material.mainTexture as Texture2D;

        modelFaceTriangulation = new Triangulation (modelFacePoints, modelTexture.width, modelTexture.height);
    }

    public void FinishedSelection()
    {
        inputFaceImage = inputImageRenderer.material.mainTexture as Texture2D;

        // Get the points corresponding to the face mask boundary
        inputFacePoints = faceElements.SelectMany(e => e.LockAndReturnPoints()).ToList();
        inputFacePoints = inputFacePoints.Select(p => ToImagePixel(p, inputFaceImage, inputImageRenderer.gameObject.transform)).ToList();

        if (inputFacePoints.Count != modelFacePoints.Count && !LearningMode)
        {
            Debug.LogError("The number of select points and the number of points given for the model does not match");
            return;
        }

        // When used on 
        if (LearningMode)
        {
            OutputLearntMapping();
        }
    }

    public void RunMapping()
    {
        if(LearningMode)
        {
            Debug.LogError("You are running the learning mode, it is only used to get the model's texture mapping given in the console");
            return;
        }

        // Make the transfer
        TransferTexture();

        // Set as texture
        GetComponent<Renderer>().material.mainTexture = modelTexture;
    }

    /// <summary>
    /// To be used on the original model's texture to create the original mapping the user's texture will be projected on
    /// </summary>
    private void OutputLearntMapping()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var point in inputFacePoints)
        {
            sb.AppendLine(string.Format("new Point({0},{1}f,{2}f),", point.id, point.x, point.y));
        }

        Debug.Log(sb.ToString());
    }

    /// <summary>
    /// Convert world coordinates to texture-local pixel coordinates
    /// </summary>
    /// <param name="p">Point with world coordinates</param>
    /// <returns>Point with image-relative pixel coordinates</returns>
    private Point ToImagePixel(Point p, Texture texture, Transform textureObjTransform)
    {
        Point res = new Point(p.id);

        //var transform = GetComponent<Transform>();
        //var texture = GetComponent<Renderer> ().material.mainTexture;

        // Transform to quad's local coordinate
        var localPos = (Vector2)textureObjTransform.worldToLocalMatrix.MultiplyPoint3x4(p.position);

        // Get local distance from bottom left corner
        var pixelLocalPos = new Vector2(0.5f, 0.5f) + new Vector2(localPos.x, localPos.y);

        // Go back to world coordinates
        var pixelWorldPos = textureObjTransform.localToWorldMatrix.MultiplyPoint3x4(pixelLocalPos);

        // Subtract quad position and un-rotate
        Vector2 pixelPos = Quaternion.Inverse(textureObjTransform.rotation) * (new Vector2(pixelWorldPos.x, pixelWorldPos.y) - new Vector2(textureObjTransform.position.x, textureObjTransform.position.y));

        // Scale to image size
        pixelPos.x = pixelPos.x * texture.width / textureObjTransform.localScale.x;
        pixelPos.y = pixelPos.y * texture.height / textureObjTransform.localScale.y;

        res.position = pixelPos;

        return res;
    }


    /// <summary>
    /// Transfer the texture
    /// </summary>
	private void TransferTexture()
	{
        // Compute the face bounding box in the model's texture to improve performances
        int minX = Mathf.FloorToInt(modelFacePoints.Min(p => p.x));
        int maxX = Mathf.CeilToInt(modelFacePoints.Max(p => p.x));
        int minY = Mathf.FloorToInt(modelFacePoints.Min(p => p.y));
        int maxY = Mathf.CeilToInt(modelFacePoints.Max(p => p.y));

        // For every pixel in destination texture
        for (int i = minX; i < maxX; i++)
		{
			for (int j = minY; j < maxY; j++)
			{
				// For each triangle in dest. triangulation
				foreach (var t in modelFaceTriangulation.triangles)
				{
					// If the pixel is inside the triangle
					if (t.IsPositionInside( new Vector2( i, j)))
					{
						// Get the barycentric coordinates
						var barycentric = t.GetBarycentricCoordinates( new Vector2( i,  j));

                        // Get the equivalent triangle in the source triangulation
                        var p1 = inputFacePoints.FirstOrDefault(p => p.id == t.points.ElementAt(0).id);
                        var p2 = inputFacePoints.FirstOrDefault(p => p.id == t.points.ElementAt(1).id);
                        var p3 = inputFacePoints.FirstOrDefault(p => p.id == t.points.ElementAt(2).id);

                        var srcTriangle = new Triangle(p1, p2, p3);

						// Get the equivalent pixel coordinates in the src triangle
						Vector2 src_pixel = srcTriangle.getCartesianCoords(barycentric);

						// Get the pixel value at source texture
						Color src_val = inputFaceImage.GetPixel( (int) src_pixel.x, (int) src_pixel.y );

						// Set the same value at dest. texture
						modelTexture.SetPixel(i, j, src_val);
						break;
					}
				}
			}
		}	
		modelTexture.Apply ();
        var modifiedTexture = modelTexture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/saved_texture.png", modifiedTexture);
        Debug.Log("HERE YOU CAN SAVE TEXTURE WITH DIALOG. Saved texture to " + Application.persistentDataPath);
	}
}
