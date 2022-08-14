uniform sampler2D texture;
 
uniform float timer;
uniform float alcohol;

void main() {
    vec2 coord = gl_TexCoord[0].xy;
    coord.x += sin(radians(timer + coord.y * alcohol)) * 0.006;
    vec4 pixel_color = texture2D(texture, coord);
 
    gl_FragColor = pixel_color;
}