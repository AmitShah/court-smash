using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIConvexHull;
using UnityEngine;



	/// <summary>
	/// Represents a tetrahedron - what a surprise.
	/// </summary>
	class Tetrahedron : TriangulationCell<Vertex, Tetrahedron>
	{
		// easing functions


		/// <summary>
		/// Helper function to get the position of the i-th vertex.
		/// </summary>
		/// <param name="i"></param>
		/// <returns>Position of the i-th vertex</returns>
		Vector3 GetPosition(int i)
		{
			return new Vector3 ((float)Vertices [i].Position [0], (float)Vertices [i].Position [1], (float)Vertices [i].Position [2]);
		}






		/// <summary>
		/// This function adds indices for a triangle representing the face.
		/// The order is in the CCW (counter clock wise) order so that the automatically calculated normals point in the right direction.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <param name="k"></param>
		/// <param name="center"></param>
		/// <param name="indices"></param>
	void MakeFace(int i, int j, int k, Vector3 center, List<int> indices)
		{
			var u = GetPosition(j) - GetPosition(i);
			var v = GetPosition(k) - GetPosition(j);

			// compute the normal and the plane corresponding to the side [i,j,k]
			var n = Vector3.Cross(u, v);
			var d = -Vector3.Dot(n, center);

			// check if the normal faces towards the center
			var t = Vector3.Dot(n, GetPosition(i)) + d;            
			if (t >= 0)
			{
				// swapping indices j and k also changes the sign of the normal, because cross product is anti-commutative
				indices.Add(k); indices.Add(j); indices.Add(i);
			}
			else
			{
				// indices are in the correct order
				indices.Add(i); indices.Add(j); indices.Add(k);
			}
		}

		/// <summary>
		/// Creates a model of the tetrahedron. Transparency is applied to the color.
		/// </summary>
		/// <param name="color"></param>
		/// <param name="radius"></param>
		/// <returns>A model representing the tetrahedron</returns>
//		public Model3D CreateModel(Color color, double radius)
//		{
//			this.translation = new TranslateTransform3D();
//
//			var points = new Point3DCollection(Enumerable.Range(0, 4).Select(i => GetPosition(i)));
//
//			// center = Sum(p[i]) / 4
//			var center = points.Aggregate(new Vector3D(), (a, c) => a + (Vector3D)c) / (double)points.Count;
//
//			var normals = new Vector3DCollection();
//			var indices = new Int32Collection();
//			MakeFace(0, 1, 2, center, indices);
//			MakeFace(0, 1, 3, center, indices);
//			MakeFace(0, 2, 3, center, indices);
//			MakeFace(1, 2, 3, center, indices);
//
//			var geometry = new MeshGeometry3D { Positions = points, TriangleIndices = indices };            
//			var material = new MaterialGroup 
//			{ 
//				Children = new MaterialCollection
//				{
//					new DiffuseMaterial(new SolidColorBrush(color) { Opacity = 1.00 }),
//					// give it some shine
//					new SpecularMaterial(Brushes.LightYellow, 2.0) 
//				} 
//			};
//
//	
//			return new GeometryModel3D { Geometry = geometry, Material = material, BackMaterial = material, Transform = translation };
//		}
	}
