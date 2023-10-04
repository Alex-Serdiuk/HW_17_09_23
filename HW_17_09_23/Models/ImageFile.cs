namespace HW_17_09_23.Models;

public class ImageFile
{
	public int Id { get; set; }	
	public string FileName { get; set; }

	public string Src()
	{
		return "/uploads/" + FileName;
	}
}
