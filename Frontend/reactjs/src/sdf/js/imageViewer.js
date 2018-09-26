
var imageViewer = gE('imageViewer');

function gE(elementId)
{
	return document.getElementById(elementId);
}

imageViewer.addEventListener('click', function () {

	// hide imageViewer
	imageViewer.style.display = 'none';
	gE('image').setAttribute('src', '');
});


function initImageViewer()
{
	var imgs = document.getElementsByClassName('image-to-view');

	//console.log(imageViewer);

	for (var i = 0; i < imgs.length; i++)
	{
		var img = imgs[i];

		img.addEventListener('click', function(data) {
			var element = data.target;


			var imageSrc = element.getAttribute('src');

			// show imageViewer
			imageViewer.style.display = 'block';

			// set source for image
			gE('image').setAttribute('src', imageSrc);
		});
	}
}

initImageViewer();

