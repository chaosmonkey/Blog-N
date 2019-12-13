namespace Blogn.Models
{
	public enum Role
	{
		Owner = 0, // Can do anything -> Super User
		Administrator = 1, // Manages site -> Users, Themes, etc. (Everything but content)
		Author = 2, // Creates new content
        Editor = 3, // Edits existing content
        Publisher = 4, // Publishes content
		User = 99 // Views content, posts comments 
	}
}
