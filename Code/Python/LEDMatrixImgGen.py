# Simple rapidly made tool to make arb sized dot matrix representations of input images
# Alex McCown 5/23/21

from PIL import ImageFilter, Image, ImageDraw
import argparse


def FillCircles(x_points:int, image_size_x: int, gap: int, blur: int, image_location: str, out_file: str):
    orig = Image.open(image_location)
    size = max(orig.size)
    tmp = Image.new('RGBA', (size, size), color=(0, 0, 0, 255))
    tmp.paste(orig, (0, 0))
    orig = tmp
    del tmp
    orig = orig.resize((x_points, x_points), resample=Image.BILINEAR)
    spacing = int(image_size_x / x_points)
    image = Image.new(mode='RGBA', size=(image_size_x, image_size_x), color=(0, 0, 0, 255))
    draw = ImageDraw.Draw(image)
    gap *= 2
    for y in range(x_points):
        for x in range(x_points):
            color = orig.getpixel((x, y))
            draw.ellipse(((spacing * x), (spacing * y), (spacing * x) + spacing - gap, (spacing * y) + spacing - gap),
                         fill=color)
    image = image.filter(ImageFilter.GaussianBlur(blur))
    image.save(out_file)


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Minimum example usage: python LEDMatrixGen.py -i orig.png')
    parser.add_argument("-i",'--Input', help="Name of input file", type=str)
    parser.add_argument("-o", '--Output', help="Name of output file", default='output.png', type=str)
    parser.add_argument('-s', '--Size', help="Size of image in pixels", default=2048, type=int)
    parser.add_argument('-d', '--Dots', help="Number of dots in one axis", default=64, type=int)
    parser.add_argument('-b', '--Blur', help="Blur radius", default=1, type=int)
    parser.add_argument('-g', '--Spacing', help="Spacing between points in pixels", default=2, type=int)
    args = parser.parse_args()
    if args.Input is None:
        parser.print_help()
        exit()
    FillCircles(args.Dots, args.Size, args.Spacing, args.Blur, args.Input, args.Output)
